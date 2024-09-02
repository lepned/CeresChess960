#region Using directives

//using BenchmarkDotNet.Environments;
//using DJE.Base;
using Ceres.Base;
using Ceres.Base.Benchmarking;
using Ceres.Chess.MoveGen.Converters;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;



#endregion

#region License
/*

MIT License

Copyright(c) 2016-2017 Judd Niemann

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files(the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions :

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

#endregion

using BitBoard = System.UInt64;
//typedef uint64_t BitBoard;
// typedef uint64_t HashKey;


namespace Ceres.Chess.MoveGen.Test
{

  public static class MGMoveGenTest
  {

#if NOT
    	// see https://chessprogramming.wikispaces.com/perft+Results

#endif
    static int RAND = 33;

    // --------------------------------------------------------------------------------------------
    public static ulong CalcPerftOnPosition(string fen, int depth)
    {
      MGPosition chessPos = MGChessPositionConverter.MGChessPositionFromFEN(fen);

      ulong moveCount = 0;
      Perft(chessPos, depth, 1, null, ref moveCount);
      return moveCount;
    }

    // --------------------------------------------------------------------------------------------

    public static void RunPerftWithStatsOnPosition(string fen, int depth, ulong correctCount)
    {
      Console.Write("\r\n" + fen);
      MGPosition chessPos = MGChessPositionConverter.MGChessPositionFromFEN(fen);
      //chessPos.Dump();      
      ulong moveCount = 0;
      ulong castleCount = 0;
      ulong captures = 0;
      //add primitive timing with dotnet
      var sw = new Stopwatch();
      sw.Start();
      using (new TimingBlock("PerftCollect " + fen + " correct #" + correctCount))
      {
        moveCount = 0;
        castleCount = 0;
        captures = 0;
        PerftCollect(chessPos, depth, 1, null, ref moveCount, ref castleCount, ref captures);

      }
      sw.Stop();
      var ms = sw.ElapsedMilliseconds;
      //nodes per second
      var secs = ms / 1000.0;
      var nps = moveCount / secs;
      //get current console color
      var cc = Console.ForegroundColor;
      string msg = $"NPS = {nps.ToString("N0")}, Nodes = {moveCount} with {castleCount} castles and {captures} captures\n";
      if (moveCount != correctCount)
      {
        //write red color to console if failed
        Console.ForegroundColor = ConsoleColor.Red;
        var board = MGChessPositionConverter.MGChessPositionFromFEN(fen);
        var asciBoard = board.BoardString;
        Console.WriteLine(asciBoard);
        Console.WriteLine(" " + moveCount + " FAIL ");
        Console.WriteLine(msg);
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" " + moveCount + " SUCCESS ");
        Console.WriteLine(msg);        
      }
      Console.ForegroundColor = cc;
    }

    public static void RunPerftOnPosition(string fen, int depth, ulong correctCount)
    {
      Console.Write("\r\n" + fen);
      MGPosition chessPos = MGChessPositionConverter.MGChessPositionFromFEN(fen);
      //chessPos.Dump();      
      ulong moveCount = 0;
      //add primitive timing with dotnet
      var sw = new Stopwatch();
      sw.Start();
      using (new TimingBlock("Perft " + fen + " correct #" + correctCount))
      {
        moveCount = 0;
        Perft(chessPos, depth, 1, null, ref moveCount);

      }
      sw.Stop();
      var ms = sw.ElapsedMilliseconds;
      //nodes per second
      var secs = ms / 1000.0;
      var nps = moveCount / secs;
      //get current console color
      var cc = Console.ForegroundColor;
      if (moveCount != correctCount)
      {
        //write red color to console if failed
        Console.ForegroundColor = ConsoleColor.Red;
        var board = MGChessPositionConverter.MGChessPositionFromFEN(fen);
        var asciBoard = board.BoardString;
        Console.WriteLine(asciBoard);
        Console.WriteLine(" " + moveCount + " FAIL");
        Console.WriteLine(" " + nps.ToString("N0") + " NPS");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" " + moveCount + " SUCCESS ");
        Console.WriteLine(" " + nps.ToString("N0") + " NPS");
      }
      Console.ForegroundColor = cc;
    }

    // --------------------------------------------------------------------------------------------
    static void TestConvertPerformance()
    {
      Position posCompressedFromFEN = Position.FromFEN("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1");

      MGPosition mgPosFromPosCompressed = default;
      using (new TimingBlock("FromPositionCompressed"))
      {
        for (int i = 0; i < 1_000_000; i++)
        {
          mgPosFromPosCompressed = MGPosition.FromPosition(in posCompressedFromFEN);
        }
      }

      Position posCompressedFromMGPos1 = default;
      using (new TimingBlock("AsPositionCompressed"))
      {
        for (int i = 0; i < 1_000_000; i++)
        {
          posCompressedFromMGPos1 = mgPosFromPosCompressed.ToPosition;
        }
      }

#if NOT
      using (new TimingBlock("PieceAt"))
      {
        for (int i = 0; i < 1_000_000; i++)
        {
          for (int j = 0; j < 64; j++)
          {
            var pa = MGChessPositionConverter.PieceAt(mgPosFromPosCompressed, j);
            if ((byte)pa.Type > 222) throw new NotImplementedException();
//            var pa1 = posCompressedFromMGPos1[new Square(j)];
//            if ((byte)pa1.Kind > 222) throw new NotImplementedException();
          }
        }
      }
#endif


    }


    static ulong HASH_COLLISIONS = 0;
    // --------------------------------------------------------------------------------------------
    public class Chess960Record
    {
      public int PositionNumber { get; set; }
      public string FEN { get; set; }
      public long Depth1 { get; set; }
      public long Depth2 { get; set; }
      public long Depth3 { get; set; }
      public long Depth4 { get; set; }
      public long Depth5 { get; set; }
      public long Depth6 { get; set; }
    }

    public static Chess960Record ParseChess960Record(string input)
    {
      Console.WriteLine(input);
      var parts = input.Split('\t');
      var positionNumber = int.Parse(parts[0]);
      var fen = parts[1];
      var depth1 = long.Parse(parts[2]);
      var depth2 = long.Parse(parts[3]);
      var depth3 = long.Parse(parts[4]);
      var depth4 = long.Parse(parts[5]);
      var depth5 = long.Parse(parts[6]);
      var depth6 = long.Parse(parts[7]);

      return new Chess960Record
      {
        PositionNumber = positionNumber,
        FEN = fen,
        Depth1 = depth1,
        Depth2 = depth2,
        Depth3 = depth3,
        Depth4 = depth4,
        Depth5 = depth5,
        Depth6 = depth6
      };
    }

    public static Chess960Record[] ParseFile(string filePath)
    {
      var lines = System.IO.File.ReadAllLines(filePath).Skip(1).ToArray();
      var records = lines.Select(ParseChess960Record).ToArray();
      return records;
    }

    public static long GetDepthValue(int depth, Chess960Record record)
    {
      return depth switch
      {
        1 => record.Depth1,
        2 => record.Depth2,
        3 => record.Depth3,
        4 => record.Depth4,
        5 => record.Depth5,
        6 => record.Depth6,
        _ => throw new NotImplementedException($"Depth {depth} is not implemented.")
      };
    }


    public static void RunChess960Verification(int depth)
    {
      var path = "C:/Dev/Chess/Chess960.txt";
      var records = ParseFile(path);
      var ordered = records.OrderBy(r => GetDepthValue(depth, r));
      //RunPerftOnPosition("bbqrnnkr/1ppp1p1p/5p2/p5p1/P7/1P4P1/2PPPP1P/1BQRNNKR w HDhd - 0 9", 2, (ulong)23);

      foreach (var record in ordered)
      {
        //write record position number to console with text position number and FEN
        Console.WriteLine($"Position Number: {record.PositionNumber}");
        var perftDepth = GetDepthValue(depth, record);
        RunPerftWithStatsOnPosition(record.FEN, depth, (ulong)perftDepth);
      }

    }

    public static void RunPerftSuite()
    {
      Console.WriteLine("Running PERFT Test Suite\n\n");
      //chess960 test
      //RunPerftOnPosition("qbb1rkrn/1ppppppp/p7/7n/8/P2P4/1PP1PPPP/QBBRNKRN w Gg - 0 9", 2, 547);
      //RunPerftOnPosition("bbr1qk1n/1ppppp1p/2n5/p7/P7/1P2P3/2PP1PrP/1BRNQKRN w GCc - 0 9", 2, 520);
      RunPerftWithStatsOnPosition("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 25", 1, 48);
      RunPerftWithStatsOnPosition("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 25", 2, 2039);      
      RunPerftWithStatsOnPosition("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 25", 3, 97862);
      RunPerftWithStatsOnPosition("b1q1rrkb/pppppppp/3nn3/8/P7/1PPP4/4PPPP/BQNNRKRB w GE - 1 9", 3, 10471);
      if (false)
      {
        //RunPerftOnPosition("r1krnnbq/pp1ppp1p/6p1/2p5/2P5/P3P3/Rb1P1PPP/1BKRNNBQ w Dda - 0 9", 2, 61);
        //RunPerftOnPosition("brnk1qrb/p1ppppp1/1p5p/8/P3n3/1N4P1/1PPPPPRP/BR1KNQ1B w Bgb - 0 9", 1, 22);
        //RunPerftOnPosition("rk2bnqb/pprpppp1/4n2p/2p5/P7/3P2NP/1PP1PPP1/RKRNB1QB w CAa ", 2, 596);
        //RunPerftOnPosition("r3k2r/p1ppqNb1/bn2pnp1/3P4/1p2P3/2N2Q1p/PPPBBPPP/R3K2R b KQkq - 0 1", 1, 40);
        //RunPerftOnPosition("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 1, 48);
        //RunPerftOnPosition("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 2, 2039);
        //RunPerftOnPosition("qbb1rkrn/1ppppppp/p7/7n/8/P2P4/1PP1PPPP/QBBRNKRN w Gg - 0 9", 2, 547);
        //RunPerftOnPosition("nbbrnkr1/1pppp1p1/p6q/P4p1p/8/5P2/1PPPP1PP/NBBRNRKQ w gd - 2 9", 2, 556);
        //RunPerftOnPosition("rkbbn1nr/ppppp1pp/8/6N1/5p2/1q6/P1PPPPPP/RKBBN1QR w HAha", 2, 72);
        //RunPerftOnPosition("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w HAha - 0 1", 6, 119060324); //Start Position
        //RunPerftOnPosition("rnkrbbq1/pppppnp1/7p/8/1B1Q1p2/3P1P2/PPP1P1PP/RNKR1B1N w DAda - 2 9", 6, 851927292);
        //RunPerftOnPosition("br1krnqb/pppppp1p/1n4p1/8/8/P2NN3/2PPPPPP/BR1K1RQB w Beb - 2 9", 4, 1025712);
      }

      if (false)
      {
        // These copied from QBB-Perft project on github
        // which has very fast move generator.

        //RunPerftOnPosition("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 6, 119060324); //Start Position
        RunPerftOnPosition("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 2, 2039);
        RunPerftOnPosition("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1", 7, 178633661);
        RunPerftOnPosition("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 6, 706045033);
        RunPerftOnPosition("rnbqkb1r/pp1p1ppp/2p5/4P3/2B5/8/PPP1NnPP/RNBQK2R w KQkq - 0 6", 3, 53392);
        RunPerftOnPosition("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 5, 164075551);
      }
      RunPerftWithStatsOnPosition("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3, 8_902);        // Position 1: Initial Position
      RunPerftWithStatsOnPosition("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 5, 4_865_609);        // Position 1: Initial Position
      RunPerftWithStatsOnPosition("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 6, 119_060_324);        // Position 1: Initial Position
      

      RunPerftWithStatsOnPosition("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", 5, 9_771_632);     // DJE test (initial position after e4, test en passant)

      RunPerftWithStatsOnPosition("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 7, 3_195_901_860);        // Position 1: Initial Position
                                                                                                               //  Console.WriteLine("Hash collisions " + HASH_COLLISIONS);
                                                                                                               //  System.Environment.Exit(3);

      RunPerftWithStatsOnPosition("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 25", 5, 193_690_690);  // Position 2: 'Kiwipete' position
      RunPerftOnPosition("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0", 7, 178_633_661);                // Position 3
      RunPerftWithStatsOnPosition("r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 6, 706_045_033);   // Position 4
      System.Environment.Exit(3);

      RunPerftWithStatsOnPosition("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 6, 706_045_033);   // Position 4 Mirrored (should be same score as previous)
      RunPerftWithStatsOnPosition("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 5, 89_941_194);       // Position 5
      // too slow dumpPerftScoreFfromFEN("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 7, 287188994746); // Position 6 28/1/2016: Correct (took 8_454_195 ms)

    }

    // --------------------------------------------------------------------------------------------
    static void MovegenTest()
    {
      // NOTE: speed seems about 1,000,000 per second per CPU
      const int NUM_POS = 100_000;
      MGPosition mp = MGChessPositionConverter.MGChessPositionFromFEN(Position.StartPosition.FEN);

      MGMoveList moves = new MGMoveList();
      using (new TimingBlock("GenerateMoves " + NUM_POS))
      {
        for (int i = 0; i < NUM_POS; i++)
        {
          moves.Clear();
          MGMoveGen.GenerateMoves(mp, moves);
          //        if (MGMoveGen.IsInCheck(mp, false))
          //          throw new Exception("bad");
        }
      }

    }


    // --------------------------------------------------------------------------------------------
    // Single core, June 2019
    // 3_195_901_860 perft(7) in 150 seconds
    //   119_060_324 perft(6) in 6 seconds
    //     4_865_609 perft(5) in 0.24 seconds
    public static void Test()
    {
      //MovegenTest(); //MovegenTest();
      RunChess960Verification(3);
      //RunPerftSuite();
      return;

      TestConvertPerformance();
      TestConvertPerformance();
      TestConvertPerformance();
      System.Environment.Exit(3);

      string pgnName = @"Z:\chess\data\pgn\raw\ficsgamesdb_2011_standard_nomovetimes_1506667.pgn"; // 9GB
#if NOT
      string[] fens = PGNEnumerator.GetFENSOfFirstMatchingPositionInGame(pgnName, 1000, p => true);

      int count = 0;
      DateTime lastTime = default;
      PGNEnumerator.ProcessGamesFromFile(pgnName,
   ( gameIndex, plyWithinGame,  totalPlyCount,   posSF) =>
   {
     // TO DO: See FENFromBitboard for ideas of how to do reverse operation
     //Console.WriteLine(pos.BoardPicture);

     string inFEN = posSF.fen();
     string outFEN = null;

     PositionCompressed pos;
     try
     {
       count++;
       pos = MGChessPositionConverter.PositionCompressedFromMGChessPosition(MGChessPositionConverter.MGChessPositionFromFEN(inFEN));
       outFEN = FENGenerator.GetFEN(pos);
       if (++count % 10_000 == 0)
       {
         if (lastTime != default(DateTime))
           Console.WriteLine($" {inFEN} {count} {(DateTime.Now - lastTime).TotalSeconds} {System.GC.CollectionCount(0) } { System.GC.GetAllocatedBytesForCurrentThread() / 1_000_000}");
         lastTime = DateTime.Now;
       }
     }
     catch (Exception e)
     {
       Console.WriteLine(e);
     }
     if (inFEN != outFEN)
     {
       Console.WriteLine();
       Console.WriteLine(inFEN);
       Console.WriteLine(outFEN);
     }
//     else
//       Console.Write('.');
     return false; // do not exit

   }
  );
#endif
      return;


      // DOCUMENTATION STRUCTS
      // https://docs.microsoft.com/en-us/dotnet/csharp/write-safe-efficient-code

      System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.AboveNormal;
      MGPosition startpos = new MGPosition();
      startpos.A = 5404038077867949898;
      startpos.B = 4323455642275676220;
      startpos.C = 15780613094306218203;
      startpos.D = 18446462598732840960;
      startpos.Flags = (MGPosition.FlagsEnum)30;
      startpos.MoveNumber = 1;
      //startpos.HalfMoves = 0;
      //startpos.material = 0;

      if (true)
      {
        const int DEPTH = 5; // was 6 (or 5)
        ulong moveCount = 0;
        Perft(startpos, DEPTH, 1, null, ref moveCount);
        using (new TimingBlock("Perft"))
        {
          if (false)
          {
            moveCount = 0;
            Perft(startpos, DEPTH, 1, null, ref moveCount);
            moveCount = 0;
            Perft(startpos, DEPTH, 1, null, ref moveCount);
          }
          moveCount = 0;
          Perft(startpos, DEPTH, 1, null, ref moveCount);
          Console.WriteLine(moveCount);
        }
        return;
      }


      MGPosition bug = new MGPosition();
      bug.A = 5404038077868277576;
      bug.B = 4323455642275676220;
      bug.C = 15780613094306545881;
      bug.D = 18446462598732840960;
      bug.Flags = (MGPosition.FlagsEnum)30;
      bug.Dump();

      const int MOVELIST_SIZE = 128;

      MGMoveList moves = new MGMoveList(128);

      for (int j = 0; j < 2; j++)
        using (new TimingBlock("ChessMove"))
        {
          for (int i = 0; i < 1_000_000; i++)
          {
            moves.NumMovesUsed = 0;
            moves.MovesArray[0].Flags = 0;

            MGMoveGen.GenerateMoves(bug, moves);
          }
        }

      Console.WriteLine("Num generated: " + moves.NumMovesUsed);

      for (int i = 0; i < moves.NumMovesUsed; i++)
        Console.WriteLine(i + ": " + moves.MovesArray[i].MoveStr(MGMoveNotationStyle.CoOrdinate) + " " + moves.MovesArray[i].FromSquareIndex + " " + moves.MovesArray[i].ToSquareIndex + " " + moves.MovesArray[i].Flags);
      System.Environment.Exit(3);



      MGPosition moveBlack = new MGPosition();
      moveBlack.A = 351289571361211392;
      moveBlack.B = 1441714830716174624;
      moveBlack.C = 1441151949483016224;
      moveBlack.D = 1504765229790396416;
      moveBlack.BlackToMove = true;

      MGPosition promoteBlack = new MGPosition();
      promoteBlack.A = 351289571361211904;
      promoteBlack.B = 1441714830716174624;
      promoteBlack.C = 1441151949483016224;
      promoteBlack.D = 1504765229790396928;
      promoteBlack.BlackToMove = true;


      RAND = new Random().Next();

      BitBoard bax1 = (ulong)RAND;
      BitBoard bax2 = (ulong)new Random().Next();
      BitBoard bax0 = (ulong)new Random().Next();

      // This takes 2.5 seconds for 100,000,000 in C++
      // Takes about 3.0 seconds in C#
      // ChessPositionMoveGen.isWhiteInCheck(startpos))

      if (false)
      {
        int zz = 0;
        for (int j = 0; j < 2; j++)
          using (new TimingBlock("ChessMove"))
          {
            for (int i = 0; i < 100_000_000; i++)
            {
              //              if (ChessPositionMoveGen.isWhiteInCheck(startpos)) return;
#if NOT
              ChessMove cdd = new ChessMove();
              cdd.Piece = 3;
              cdd.Castle = true;
              cdd.MoveCount = 12;
              Funky(cdd);
              if ((cdd.Castle ? 1 : 0) + cdd.Piece + cdd.MoveCount != RAND) zz++;
              if (!cdd.Castle) return;
              if (cdd.MoveCount == 4) return;
#endif
            }
            //Console.WriteLine(zz);
            //Debug.Assert((int)cdd.Flags == 204472352);
          }
      }

      return;

      //      Debug.Assert(!ChessPositionMoveGen.isWhiteInCheck(startpos));
      //      Debug.Assert(!ChessPositionMoveGen.isBlackInCheck(startpos));

    }

    static Dictionary<ulong, MGPosition> hashtable = new Dictionary<ulong, MGPosition>();

    const bool TRACK_HASH = true;

    static void PerftCollect(in MGPosition P, int maxdepth, int depth, MGMoveList movesThisDepth, ref ulong nodeCount, ref ulong castleCount, ref ulong captures)
    {
      if (movesThisDepth == null) movesThisDepth = new MGMoveList(128);
      movesThisDepth.NumMovesUsed = 0;

      MGMoveGen.GenerateMoves(in P, movesThisDepth);

      if (depth == maxdepth)
      {
        nodeCount += (ulong)movesThisDepth.NumMovesUsed;
        for (int i = 0; i < movesThisDepth.NumMovesUsed; i++)
        {
          var m = movesThisDepth.MovesArray[i];
          if (m.IsCastle)
            castleCount++;
          if (m.Capture)
            captures++;
          if(m.EnPassantCapture)
            captures++;
        }
      }
      else if (depth < maxdepth)
      {
        MGMoveList movesNextDepth = new MGMoveList(128);
        for (int i = 0; i < movesThisDepth.NumMovesUsed; i++)
        {
          MGPosition Q = new MGPosition(P);
          var m = movesThisDepth.MovesArray[i];          
          Q.MakeMove(m);
          PerftCollect(in Q, maxdepth, depth + 1, movesNextDepth, ref nodeCount, ref castleCount, ref captures);
        }
      }

      //return nodeCount;
    }

    static ulong Perft(in MGPosition P, int maxdepth, int depth, MGMoveList movesThisDepth, ref ulong nodecount)
    {
      if (movesThisDepth == null) movesThisDepth = new MGMoveList(128);
      movesThisDepth.NumMovesUsed = 0;


      MGMoveGen.GenerateMoves(in P, movesThisDepth);
      if (false)
      {
        Console.WriteLine(depth + " " + movesThisDepth.NumMovesUsed);
        P.Dump();
        Console.WriteLine(" " + P.A);
        Console.WriteLine(" " + P.B);
        Console.WriteLine(" " + P.C);
        Console.WriteLine(" " + P.D);
        Console.WriteLine(" " + P.Flags);
        Console.WriteLine();
      }

      if (depth == maxdepth)
      {
        nodecount += (ulong)movesThisDepth.NumMovesUsed;
#if MG_USE_HASH
        if (TRACK_HASH)
        {
          if (hashtable.ContainsKey(P.HK))
          {
            if (hashtable[P.HK] != P)
              HASH_COLLISIONS++;
          }
          hashtable[P.HK] = P;
        }
#endif
      }
      else
      {
        MGMoveList movesNextDepth = new MGMoveList(128);
        //nodecount += (ulong)movesThisDepth.NumMovesUsed;
        for (int i = 0; i < movesThisDepth.NumMovesUsed; i++)
        {
          MGPosition Q = new MGPosition(P);
          var m = movesThisDepth.MovesArray[i];
          Q.MakeMove(m);

#if MG_USE_HASH
          if (false)
          {
//            if (i == 0) Console.WriteLine();
            //// ---------------------------------------------------
            //// Do this to trap bugs with incremental Hash updates:
            MGChessPosition Q2 = Q;
            Q2.CalculateHash();

            if (Q2.HK != Q.HK)
            {
              Console.WriteLine("\r\nbad " + movesThisDepth.MovesArray[i]);
              Q.Dump();
              Q2.Dump();
              Console.WriteLine();
            }
//            else
//              Console.WriteLine("ok " + movesThisDepth.Moves[i].MoveStr());
            //// ---------------------------------------------------
          }
#endif

          Perft(in Q, maxdepth, depth + 1, movesNextDepth, ref nodecount);//, pI);
        }
        //Q = P; // unmake move
      }

      return nodecount;
    }






  }
}


