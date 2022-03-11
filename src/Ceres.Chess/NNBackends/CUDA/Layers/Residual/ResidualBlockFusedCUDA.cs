﻿#region License notice

/*
  This file is part of the Ceres project at https://github.com/dje-dev/ceres.
  Copyright (C) 2020- by David Elliott and the Ceres Authors.

  Ceres is free software under the terms of the GNU General Public License v3.0.
  You should have received a copy of the GNU General Public License
  along with Ceres. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

#region Using directives

using System;
using ManagedCuda;
using ManagedCuda.BasicTypes;

using Ceres.Base.CUDA;
using Ceres.Base.DataTypes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Ceres.Base.Math;

#endregion

// NOTE: This file is a highly derivative of the LC0 CUDA backend source code in the Leela Chess Zero project
//       constituting largely a transliteration of C++ code into C#, with certain enhancements.

namespace Ceres.Chess.NNBackends.CUDA
{
  public class ResidualBlockFusedCUDA : ResidualBlockBaseCUDA
  {
    // The launcher preallocates and pins the parameters to pass
    // reducing memory allocations (considerably) and 
    // improving speed (slightly).
    public const bool USE_LAUNCHER = true;

    const int kOpInpTransformBlockSize = 64;
    const int kMaxResBlockFusingChannels = 384;  // limit on num_filters
    const int kMaxResBlockFusingSeKFp16Ampere = 512;  // (use a different kernel with reduced register pressure)
    const int kMaxResBlockFusingSeK = 128;  // limit on (num_filters / se_ratio)
    const int kMaxResBlockFusingSeFp16AmpereSmem = 72 * kMaxResBlockFusingSeKFp16Ampere * 2; // Marshal.SizeOf<FP16>();  // shared memory used by the special kernel

    bool IsBig => C > 384;


    const string knInput = "_ZN6lczero13cudnn_backend21InputTransform_kernelI6__halfLb1EEEviiPKT_PS3_";

    static string BoolStr(bool b) => b ? "1" : "0";

    static string GetOutputTransformKernelName(bool useSE, ActivationFunction activation, bool useBias, bool useSkip, bool skipInputNHCW, bool outputNHCW)
    {
      //template <typename T, bool use_se, bool relu, bool use_bias, bool use_skip, bool skipInput_nhcw, bool output_nhcw>
      //fp16_kernels.ptx:.visible .entry _ZN6lczero13cudnn_backend22OutputTransform_kernelI6__halfLb1ELNS0_18ActivationFunctionE1ELb1ELb1ELb0ELb0EEEviiiPT_PKS4_S7_S7_S7_S7_S7_S7_

      const string CALL = "_ZN6lczero13cudnn_backend22OutputTransform_kernelI6__halfLb!1ELNS0_18ActivationFunctionE!2ELb!3ELb!4ELb!5ELb!6EEEviiiPT_PKS4_S7_S7_S7_S7_S7_S7_";
      return CALL.Replace("!1", BoolStr(useSE))
                 .Replace("!2", ((int)activation).ToString())
                 .Replace("!3", BoolStr(useBias))
                 .Replace("!4", BoolStr(useSkip))
                 .Replace("!5", BoolStr(skipInputNHCW))
                 .Replace("!6", BoolStr(outputNHCW));
    }

    int NUM_SHARED_BYTES => IsBig ? (72 * 1024) : 0;  // SharedMemSize

    public override void LoadKernels()
    {
    }


    public readonly bool HasSE;
    public readonly int SEK;
    public readonly bool FirstBlock;
    public readonly bool LastBlock;

    CudaDeviceVariable<FP16> biases0;
    CudaDeviceVariable<FP16> biases1;
    CudaDeviceVariable<FP16> transformedWeights0;
    CudaDeviceVariable<FP16> transformedWeights1;

    CudaKernel kernelInput; // does not depend on activation
    CudaKernel kernelOutput;


    public ResidualBlockFusedCUDA(NNBackendExecContext parent, string name, int layerIndex,
                                  BaseLayerCUDA inputLayer,
                                  int C, bool se, int se_k, bool first, bool last, int sharedMemSize, ActivationFunction activation)
      : base(parent, name, layerIndex, C, 8, 8, inputLayer, se, se_k, sharedMemSize, activation)
    {
      if (C > 512)
      {
        // This limit is definitive since data structures are sized to 512 max (see cuda_common.h).
        throw new Exception("Maximum number of channels supported is 512");
      }

      HasSE = se;
      SEK = se_k;
      FirstBlock = first;
      LastBlock = last;

      kernelInput = GetKernel(knInput);
      kernelOutput = GetKernel(GetOutputTransformKernelName(HasSE, Activation, true, true, true, false));

      if (NNBackendLC0_CUDA.BLASLT && NNBackendLC0_CUDA.BLASLT_N > 0)
      {
#if FEATURE_BLASLT
        batchMultiplier = new (Parent.Device.Context, Parent.CuBlasLT, 
                              NNBackendLC0_CUDA.BLASLT_USE_LOOP, base.C,
                              NNBackendLC0_CUDA.BLASLT_N * 4, base.C, 36);
#else
        throw new NotImplementedException();
#endif
      }
    }


    public override void LoadWeights0(CudaStream stream, float[] weights, float[] bias)
    {
      if (Parent.ReferenceLayers != null)
      {
        ResidualBlockFusedCUDA refLayer = Parent.ReferenceLayers.Layers[LayerIndex] as ResidualBlockFusedCUDA;
        biases0 = refLayer.biases0;
        transformedWeights0 = refLayer.transformedWeights0;
      }
      else
      {
        biases0 = CudaHalf(bias);
        transformedWeights0 = ToFiltered(stream, weights, C, C);
      }
    }

    public override void LoadWeights1(CudaStream stream, float[] weights, float[] bias)
    {
      if (Parent.ReferenceLayers != null)
      {
        ResidualBlockFusedCUDA refLayer = Parent.ReferenceLayers.Layers[LayerIndex] as ResidualBlockFusedCUDA;
        biases1 = refLayer.biases1;
        transformedWeights1 = refLayer.transformedWeights1;
      }
      else
      {
        biases1 = CudaHalf(bias);
        transformedWeights1 = ToFiltered(stream, weights, C, C);
      }
    }


#if FEATURE_BLASLT
    GEMMStridedBatched batchMultiplier = null;
#endif



    protected override void DoEval(CudaStream stream, int N,
                                   CudaDeviceVariable<FP16> output,
                                   CudaDeviceVariable<FP16> input, CudaDeviceVariable<FP16> input2,
                                   CudaDeviceVariable<FP16> scratch,
                                   long scratch_size,
                                   CudaDeviceVariable<FP16> scratchSecondHalf)
    {
      // normally:
      // - "output" initially contains the transformed input, 
      //    and after this layer, it contains the transformed input for next layer
      // - "input" contains the original/untransformed input
      // special cases:
      //   - for first_block_, input is real input (untransformed)
      //   - for last_block_, output is the final output of this block (untransformed)

      // Split the scratch space into two parts - use first part for holding
      // transformed input and second part for transformed output.

      CudaDeviceVariable<FP16> transformed_input = scratch;
      CudaDeviceVariable<FP16> transformed_output = scratchSecondHalf;

      #region Preparatory

      if (FirstBlock)
      {
        // Possibly duplicate code as in FusedWinogradSELayerCUDA
        kernelInput.GridDimensions = N;
        kernelInput.BlockDimensions = C;
        // InputTransform_kernel < T, nhcw > <<< N, C, 0, stream >>> (N, C, input, transformed_input);
        LaunchKernel(stream, kernelInput, N, C, input.DevicePointer, transformed_input.DevicePointer, stream.Stream.Pointer);
        cublasRowMajorMatrixMul(transformed_input, transformedWeights0, transformed_output, N * 4, C, C, 36);
      }
      else
      {
        if (true)
        {
#if FEATURE_BLASLT
        if (batchMultiplier != null)
            {
              if (N != NNBackendLC0_CUDA.BLASLT_N) throw new Exception("Wrong N");
              batchMultiplier.Execute(stream, transformedWeights0, output, transformed_output, false);
            }
            else
            {
#else
          cublasRowMajorMatrixMul(output, transformedWeights0, transformed_output, N * 4, C, C, 36);
#endif
        }
        else
        {
          throw new NotImplementedException(); // see code at bottom of file
        }
      }

      #endregion

      #region InputOutput
      OutputInputTransform(false, Activation, true, false, N, C, 0,
                           transformed_input, transformed_output,
                           null, biases0, stream, scratch);
      #endregion

      cublasRowMajorMatrixMul(transformed_input, transformedWeights1, transformed_output, N * 4, C, C, 36);

      bool allowFusing = (C <= kMaxResBlockFusingChannels)
                      || ((SharedMemSize >= kMaxResBlockFusingSeFp16AmpereSmem)
                          && (C <= kMaxResBlockFusingSeKFp16Ampere));
      
      if (LastBlock)
      {
        CUdeviceptr DUMMY = transformed_input.DevicePointer;// needs to point to something valid
        kernelOutput.GridDimensions = N;
        kernelOutput.BlockDimensions = C;
        LaunchKernel(stream, kernelOutput, N, C, SEK,
                    output.DevicePointer, transformed_output.DevicePointer,
                    input.DevicePointer, biases1.DevicePointer,
                    HasSE ? Weights1.DevicePointer : DUMMY, HasSE ? Biases1.DevicePointer : DUMMY,
                    HasSE ? Weights2.DevicePointer : DUMMY, HasSE ? Biases2.DevicePointer : DUMMY, stream.Stream.Pointer);
      }
      else
      {
        if (HasSE)
        {
          if (allowFusing)
          {
            // OutputInputTransform<DataType, true, RELU, true, true>(
            //   N, C, se_k_, output, transformed_output, input, biases1_, w1_, b1_, w2_, b2_, stream);
            OutputInputTransform(true, Activation, true, true,
                                 N, C, SEK, output, transformed_output, input, biases1,
                                 stream, scratch);
          }
          else
          {
            // OutputTransform<DataType, true, RELU, true, true, true, true>(
            //   N, C, se_k_, (DataType*)input, transformed_output, input, biases1_, w1_, b1_, w2_, b2_, stream);
            CudaKernel kernelOutputNotNCHW = GetKernel(GetOutputTransformKernelName(true, Activation, true, true, true, true));
            kernelOutputNotNCHW.GridDimensions = N;
            kernelOutputNotNCHW.BlockDimensions = C;
            LaunchKernel(stream, kernelOutputNotNCHW, N, C, SEK, input.DevicePointer, transformed_output.DevicePointer, input.DevicePointer,
                         biases1.DevicePointer,
                         Weights1.DevicePointer, Biases1.DevicePointer,
                         Weights2.DevicePointer, Biases2.DevicePointer,
                         stream.Stream.Pointer);

            // InputTransform<DataType, true>(N, C, output, (DataType*)input, stream);
            kernelInput.GridDimensions = N;
            kernelInput.BlockDimensions = C;
            LaunchKernel(stream, kernelInput, N, C, input.DevicePointer, output.DevicePointer, stream.Stream.Pointer);
          }
        }
        else
        {
          OutputInputTransform(false, Activation, true, true,
                               N, C, SEK, output, transformed_output, input, biases1,
                               stream, scratch);
        }

      }


    }

    const bool AVOID_GetOIKernelSE_RELU = false;

    //  public template<typename T = half, bool use_se, ActivationFunction activation, bool use_bias, bool use_skip>
    void OutputInputTransform(bool useSE, ActivationFunction activation, bool useBias, bool useSkip,
                              int N, int C, int sek,
                              CudaDeviceVariable<FP16> output, CudaDeviceVariable<FP16> input,
                              CudaDeviceVariable<FP16> skip, CudaDeviceVariable<FP16> bias,                               
                              CudaStream stream, CudaDeviceVariable<FP16> dummy)
    {
      // Each thread processes entire chess board.
      if (!useSE)
      {
        CudaKernel kernelRELU = GetKernel(GetOIKernelRELU(Activation, useBias, useSkip));
        //        throw new Exception("set <<grid_dim, kOpInpTransformBlockSize, 0, stream>>>  ");
// ???????? Are GridDimensions/BlockDimensions swapped?
        kernelRELU.GridDimensions =  new ManagedCuda.VectorTypes.dim3((int)MathUtils.RoundedUp(C, kOpInpTransformBlockSize), N, 1);
        kernelRELU.BlockDimensions = kOpInpTransformBlockSize;
        LaunchKernel(stream, kernelRELU, N, C, 
                     output.DevicePointer, input.DevicePointer,
                     CUPtr(skip, dummy), CUPtr(bias, dummy));
#if NOT
          dim3 grid_dim(DivUp(C, kOpInpTransformBlockSize), N, 1);
          OutputTransform_relu_InputTransform_kernel
            <half, activation, use_bias, use_skip>
            <<<grid_dim, kOpInpTransformBlockSize, 0, stream>>> 
            (N, C, output, input, (half*)skip, bias);
#endif
      }
      else if (AVOID_GetOIKernelSE_RELU || C > 384) //kMaxResBlockFusingChannels
      {
        // Use special kernel with reduced register pressure - only works on Ampere
        if (C <= 512) // kMaxResBlockFusingSeKFp16Ampere
        {
          // max supported filter count for fast path

          CudaKernel kernelSHMEM = GetKernel(GetOIKernelNameShmem(Activation, useBias, useSkip));

          CUdeviceptr DUMMY = default;
          if (!base.HasSE)
          {
            // Although not actually used in non-SE mode,
            // we have to pass some valid address to the kernel to prevent error.
            DUMMY = default;// scratch.DevicePointer;
          }

          kernelSHMEM.GridDimensions = N;
          kernelSHMEM.BlockDimensions = C;


          int numSharedBytes = 0;
          if (!LastBlock)
          {
            numSharedBytes = NUM_SHARED_BYTES;
            kernelSHMEM.MaxDynamicSharedSizeBytes = NUM_SHARED_BYTES;
          }

          if (AVOID_GetOIKernelSE_RELU)
          {
            numSharedBytes = (72 * 1024);
            kernelSHMEM.MaxDynamicSharedSizeBytes = numSharedBytes;
          }

          // Necessary to use a launcher because only in that way
          // is opportunity to set shared memory available.
          CUDAKernelLauncher launcher;
          launcher = new(kernelSHMEM, stream, numSharedBytes,
                          new object[] {N, C, sek,
                                        output.DevicePointer, input.DevicePointer,
                                        skip.DevicePointer, biases1.DevicePointer,
                                        HasSE ? Weights1.DevicePointer : DUMMY, HasSE ? Biases1.DevicePointer : DUMMY,
                                        HasSE ? Weights2.DevicePointer : DUMMY, HasSE ? Biases2.DevicePointer : DUMMY,
                                        stream.Stream.Pointer});

//          launcher.Parms.ObjRef<int>(0) = N;
//          launcher.Parms.ObjRef<int>(1) = C;
          launcher.LaunchAsync();
//stream.Synchronize();

#if NOT
            cudaFuncSetAttribute(OutputInputTransformKernel_fp16_shmem_board<activation, use_bias, use_skip>,
                                 cudaFuncAttributeMaxDynamicSharedMemorySize, 72 * C * sizeof(half));
            OutputInputTransformKernel_fp16_shmem_board
              <activation,use_bias, use_skip>
              <<<N, C, kMaxResBlockFusingSeFp16AmpereSmem, stream >>> 
              (N, C, se_K, (half*)output, (const half*)input, (half*)skip,
              (half*)bias, (half*)w1, (half*)b1, (half*)w2, (half*)b2);
#endif
        }
        else
        {
          throw new Exception("Residual block fusing opt not supported for the given data type and no of filters");
        }
      }
      else
      {
        CudaKernel kernelSE_RELU = GetKernel(GetOIKernelSE_RELU(Activation, useBias, useSkip));
        kernelSE_RELU.GridDimensions = N;
        kernelSE_RELU.BlockDimensions = C;
//        throw new Exception("set <<<N, C, 0, stream>>> ");
        LaunchKernel(stream, kernelSE_RELU, N, C, sek,
                     output.DevicePointer, input.DevicePointer,
                     skip.DevicePointer, biases1.DevicePointer,
                     Weights1.DevicePointer, Biases1.DevicePointer, Weights2.DevicePointer, Biases2.DevicePointer,
                     stream.Stream.Pointer);
//stream.Synchronize();
#if NOT
        CudaKernel kernel = 
        OutputTransform_SE_relu_InputTransform_kernel
            <half, activation, use_bias, use_skip>
            <<<N, C, 0, stream>>> 
            (N, C, se_K, output, input, (half*)skip, bias, w1, b1, w2, b2);
#endif
      }



    }


#region Kernel name helpers

    static object CUPtr(CudaDeviceVariable<FP16> var) => var == null ? (IntPtr)0 : var.DevicePointer;
    static object CUPtr(CudaDeviceVariable<FP16> var, CudaDeviceVariable<FP16> dummy) 
      => var == null ? dummy.DevicePointer : var.DevicePointer;
    CudaKernel GetKernel(string kernelName) => Parent.Device.GetKernel(Parent.PTXAssembly, FP16_KERNELS_PTX_NAME, kernelName);

    static string GetOIKernelNameShmem(ActivationFunction activation, bool useBias, bool useSkip)
    {
      const string CALL = "_ZN6lczero13cudnn_backend43OutputInputTransformKernel_fp16_shmem_boardILNS0_18ActivationFunctionE!1ELb!2ELb!3EEEviiiP6__halfPKS3_S4_S6_S6_S6_S6_S6_";
      return CALL.Replace("!1", ((int)activation).ToString())
                 .Replace("!2", BoolStr(useBias))
                 .Replace("!3", BoolStr(useSkip));
    }

    static string GetOIKernelRELU(ActivationFunction activation, bool useBias, bool useSkip)
    {
      const string CALL = "_ZN6lczero13cudnn_backend42OutputTransform_relu_InputTransform_kernelI6__halfLNS0_18ActivationFunctionE!1ELb!2ELb!3EEEviiPT_PKS4_S5_S7_";
      return CALL.Replace("!1", ((int)activation).ToString())
                 .Replace("!2", BoolStr(useBias))
                 .Replace("!3", BoolStr(useSkip));
    }

    static string GetOIKernelSE_RELU(ActivationFunction activation, bool useBias, bool useSkip)
    {
      //_ZN6lczero13cudnn_backend45OutputTransform_SE_relu_InputTransform_kernelI6__halfLNS0_18ActivationFunctionE1ELb1ELb1EEEviiiPT_PKS4_S5_S7_S7_S7_S7_S7_(
      //_ZN6lczero13cudnn_backend45OutputTransform_SE_relu_InputTransform_kernelI6__halfLNS0_18ActivationFunctionE1ELb1ELb0EEEviiiPT_PKS4_S5_S7_S7_S7_S7_S7_(
      //_ZN6lczero13cudnn_backend45OutputTransform_SE_relu_InputTransform_kernelI6__halfLNS0_18ActivationFunctionE5ELb1ELb1EEEviiiPT_PKS4_S5_S7_S7_S7_S7_S7_(
      //_ZN6lczero13cudnn_backend45OutputTransform_SE_relu_InputTransform_kernelI6__halfLNS0_18ActivationFunctionE5ELb1ELb0EEEviiiPT_PKS4_S5_S7_S7_S7_S7_S7_(
                         //_ZN6lczero13cudnn_backend45OutputTransform_SE_relu_InputTransform_kernelI6__halfLNS0_18ActivationFunctionE5 ELb1 ELb1  EEEviiiPT_PKS4_S5_S7_S7_S7_S7_S7_
      const string CALL = "_ZN6lczero13cudnn_backend45OutputTransform_SE_relu_InputTransform_kernelI6__halfLNS0_18ActivationFunctionE!1ELb!2ELb!3EEEviiiPT_PKS4_S5_S7_S7_S7_S7_S7_";
      return CALL.Replace("!1", ((int)activation).ToString())
                  .Replace("!2", BoolStr(useBias))
                  .Replace("!3", BoolStr(useSkip));
    }

#endregion
  }


}


#if NOT
          using (new TimingBlock("10_000"))
          {
            for (int i = 0; i < 1000_000; i++)
            {
              cublasRowMajorMatrixMul(output, transformed_weights0_, transformed_output, N * 4, C, C, 36);

              FP16[] correct = new FP16[384 * 1024 * 36];
              transformed_output.CopyToHost(correct);

              batchMultiplier.Execute(stream, transformed_weights0_, output, transformed_output, false);

              FP16[] attempt = new FP16[384 * 1024 * 36];
              transformed_output.CopyToHost(attempt);
              for (int ix = 0; ix < attempt.Length; ix++)
              {
                if (Math.Abs(correct[ix] - attempt[ix]) > 1E-5)
                {
                  Console.WriteLine("bad ");
                }
              }
              Console.WriteLine("ok");
            }
          }
#endif


