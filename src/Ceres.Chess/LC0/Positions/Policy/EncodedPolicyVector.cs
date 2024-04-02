#region License notice

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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Ceres.Chess.EncodedPositions.Basic;
#endregion

namespace Ceres.Chess.EncodedPositions
{
  /// <summary>
  /// Represents a policy vector output by a neural network
  /// as an array of 1858 floats (in  the Leela Chess Zero format).
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public readonly unsafe struct EncodedPolicyVector : IEquatable<EncodedPolicyVector>
  {
    public const int POLICY_VECTOR_LENGTH = 1858;

    #region Raw structure data

    // Policy vector. Ideally we would use a fixed buffer, but this is not allowed in C# 7 (see above)
    //    public fixed float Probabilities[POLICY_VECTOR_LENGTH]; 
    public readonly float Probability_0;
    public readonly float Probability_1;
    public readonly float Probability_2;
    public readonly float Probability_3;
    public readonly float Probability_4;
    public readonly float Probability_5;
    public readonly float Probability_6;
    public readonly float Probability_7;
    public readonly float Probability_8;
    public readonly float Probability_9;
    public readonly float Probability_10;
    public readonly float Probability_11;
    public readonly float Probability_12;
    public readonly float Probability_13;
    public readonly float Probability_14;
    public readonly float Probability_15;
    public readonly float Probability_16;
    public readonly float Probability_17;
    public readonly float Probability_18;
    public readonly float Probability_19;
    public readonly float Probability_20;
    public readonly float Probability_21;
    public readonly float Probability_22;
    public readonly float Probability_23;
    public readonly float Probability_24;
    public readonly float Probability_25;
    public readonly float Probability_26;
    public readonly float Probability_27;
    public readonly float Probability_28;
    public readonly float Probability_29;
    public readonly float Probability_30;
    public readonly float Probability_31;
    public readonly float Probability_32;
    public readonly float Probability_33;
    public readonly float Probability_34;
    public readonly float Probability_35;
    public readonly float Probability_36;
    public readonly float Probability_37;
    public readonly float Probability_38;
    public readonly float Probability_39;
    public readonly float Probability_40;
    public readonly float Probability_41;
    public readonly float Probability_42;
    public readonly float Probability_43;
    public readonly float Probability_44;
    public readonly float Probability_45;
    public readonly float Probability_46;
    public readonly float Probability_47;
    public readonly float Probability_48;
    public readonly float Probability_49;
    public readonly float Probability_50;
    public readonly float Probability_51;
    public readonly float Probability_52;
    public readonly float Probability_53;
    public readonly float Probability_54;
    public readonly float Probability_55;
    public readonly float Probability_56;
    public readonly float Probability_57;
    public readonly float Probability_58;
    public readonly float Probability_59;
    public readonly float Probability_60;
    public readonly float Probability_61;
    public readonly float Probability_62;
    public readonly float Probability_63;
    public readonly float Probability_64;
    public readonly float Probability_65;
    public readonly float Probability_66;
    public readonly float Probability_67;
    public readonly float Probability_68;
    public readonly float Probability_69;
    public readonly float Probability_70;
    public readonly float Probability_71;
    public readonly float Probability_72;
    public readonly float Probability_73;
    public readonly float Probability_74;
    public readonly float Probability_75;
    public readonly float Probability_76;
    public readonly float Probability_77;
    public readonly float Probability_78;
    public readonly float Probability_79;
    public readonly float Probability_80;
    public readonly float Probability_81;
    public readonly float Probability_82;
    public readonly float Probability_83;
    public readonly float Probability_84;
    public readonly float Probability_85;
    public readonly float Probability_86;
    public readonly float Probability_87;
    public readonly float Probability_88;
    public readonly float Probability_89;
    public readonly float Probability_90;
    public readonly float Probability_91;
    public readonly float Probability_92;
    public readonly float Probability_93;
    public readonly float Probability_94;
    public readonly float Probability_95;
    public readonly float Probability_96;
    public readonly float Probability_97;
    public readonly float Probability_98;
    public readonly float Probability_99;
    public readonly float Probability_100;
    public readonly float Probability_101;
    public readonly float Probability_102;
    public readonly float Probability_103;
    public readonly float Probability_104;
    public readonly float Probability_105;
    public readonly float Probability_106;
    public readonly float Probability_107;
    public readonly float Probability_108;
    public readonly float Probability_109;
    public readonly float Probability_110;
    public readonly float Probability_111;
    public readonly float Probability_112;
    public readonly float Probability_113;
    public readonly float Probability_114;
    public readonly float Probability_115;
    public readonly float Probability_116;
    public readonly float Probability_117;
    public readonly float Probability_118;
    public readonly float Probability_119;
    public readonly float Probability_120;
    public readonly float Probability_121;
    public readonly float Probability_122;
    public readonly float Probability_123;
    public readonly float Probability_124;
    public readonly float Probability_125;
    public readonly float Probability_126;
    public readonly float Probability_127;
    public readonly float Probability_128;
    public readonly float Probability_129;
    public readonly float Probability_130;
    public readonly float Probability_131;
    public readonly float Probability_132;
    public readonly float Probability_133;
    public readonly float Probability_134;
    public readonly float Probability_135;
    public readonly float Probability_136;
    public readonly float Probability_137;
    public readonly float Probability_138;
    public readonly float Probability_139;
    public readonly float Probability_140;
    public readonly float Probability_141;
    public readonly float Probability_142;
    public readonly float Probability_143;
    public readonly float Probability_144;
    public readonly float Probability_145;
    public readonly float Probability_146;
    public readonly float Probability_147;
    public readonly float Probability_148;
    public readonly float Probability_149;
    public readonly float Probability_150;
    public readonly float Probability_151;
    public readonly float Probability_152;
    public readonly float Probability_153;
    public readonly float Probability_154;
    public readonly float Probability_155;
    public readonly float Probability_156;
    public readonly float Probability_157;
    public readonly float Probability_158;
    public readonly float Probability_159;
    public readonly float Probability_160;
    public readonly float Probability_161;
    public readonly float Probability_162;
    public readonly float Probability_163;
    public readonly float Probability_164;
    public readonly float Probability_165;
    public readonly float Probability_166;
    public readonly float Probability_167;
    public readonly float Probability_168;
    public readonly float Probability_169;
    public readonly float Probability_170;
    public readonly float Probability_171;
    public readonly float Probability_172;
    public readonly float Probability_173;
    public readonly float Probability_174;
    public readonly float Probability_175;
    public readonly float Probability_176;
    public readonly float Probability_177;
    public readonly float Probability_178;
    public readonly float Probability_179;
    public readonly float Probability_180;
    public readonly float Probability_181;
    public readonly float Probability_182;
    public readonly float Probability_183;
    public readonly float Probability_184;
    public readonly float Probability_185;
    public readonly float Probability_186;
    public readonly float Probability_187;
    public readonly float Probability_188;
    public readonly float Probability_189;
    public readonly float Probability_190;
    public readonly float Probability_191;
    public readonly float Probability_192;
    public readonly float Probability_193;
    public readonly float Probability_194;
    public readonly float Probability_195;
    public readonly float Probability_196;
    public readonly float Probability_197;
    public readonly float Probability_198;
    public readonly float Probability_199;
    public readonly float Probability_200;
    public readonly float Probability_201;
    public readonly float Probability_202;
    public readonly float Probability_203;
    public readonly float Probability_204;
    public readonly float Probability_205;
    public readonly float Probability_206;
    public readonly float Probability_207;
    public readonly float Probability_208;
    public readonly float Probability_209;
    public readonly float Probability_210;
    public readonly float Probability_211;
    public readonly float Probability_212;
    public readonly float Probability_213;
    public readonly float Probability_214;
    public readonly float Probability_215;
    public readonly float Probability_216;
    public readonly float Probability_217;
    public readonly float Probability_218;
    public readonly float Probability_219;
    public readonly float Probability_220;
    public readonly float Probability_221;
    public readonly float Probability_222;
    public readonly float Probability_223;
    public readonly float Probability_224;
    public readonly float Probability_225;
    public readonly float Probability_226;
    public readonly float Probability_227;
    public readonly float Probability_228;
    public readonly float Probability_229;
    public readonly float Probability_230;
    public readonly float Probability_231;
    public readonly float Probability_232;
    public readonly float Probability_233;
    public readonly float Probability_234;
    public readonly float Probability_235;
    public readonly float Probability_236;
    public readonly float Probability_237;
    public readonly float Probability_238;
    public readonly float Probability_239;
    public readonly float Probability_240;
    public readonly float Probability_241;
    public readonly float Probability_242;
    public readonly float Probability_243;
    public readonly float Probability_244;
    public readonly float Probability_245;
    public readonly float Probability_246;
    public readonly float Probability_247;
    public readonly float Probability_248;
    public readonly float Probability_249;
    public readonly float Probability_250;
    public readonly float Probability_251;
    public readonly float Probability_252;
    public readonly float Probability_253;
    public readonly float Probability_254;
    public readonly float Probability_255;
    public readonly float Probability_256;
    public readonly float Probability_257;
    public readonly float Probability_258;
    public readonly float Probability_259;
    public readonly float Probability_260;
    public readonly float Probability_261;
    public readonly float Probability_262;
    public readonly float Probability_263;
    public readonly float Probability_264;
    public readonly float Probability_265;
    public readonly float Probability_266;
    public readonly float Probability_267;
    public readonly float Probability_268;
    public readonly float Probability_269;
    public readonly float Probability_270;
    public readonly float Probability_271;
    public readonly float Probability_272;
    public readonly float Probability_273;
    public readonly float Probability_274;
    public readonly float Probability_275;
    public readonly float Probability_276;
    public readonly float Probability_277;
    public readonly float Probability_278;
    public readonly float Probability_279;
    public readonly float Probability_280;
    public readonly float Probability_281;
    public readonly float Probability_282;
    public readonly float Probability_283;
    public readonly float Probability_284;
    public readonly float Probability_285;
    public readonly float Probability_286;
    public readonly float Probability_287;
    public readonly float Probability_288;
    public readonly float Probability_289;
    public readonly float Probability_290;
    public readonly float Probability_291;
    public readonly float Probability_292;
    public readonly float Probability_293;
    public readonly float Probability_294;
    public readonly float Probability_295;
    public readonly float Probability_296;
    public readonly float Probability_297;
    public readonly float Probability_298;
    public readonly float Probability_299;
    public readonly float Probability_300;
    public readonly float Probability_301;
    public readonly float Probability_302;
    public readonly float Probability_303;
    public readonly float Probability_304;
    public readonly float Probability_305;
    public readonly float Probability_306;
    public readonly float Probability_307;
    public readonly float Probability_308;
    public readonly float Probability_309;
    public readonly float Probability_310;
    public readonly float Probability_311;
    public readonly float Probability_312;
    public readonly float Probability_313;
    public readonly float Probability_314;
    public readonly float Probability_315;
    public readonly float Probability_316;
    public readonly float Probability_317;
    public readonly float Probability_318;
    public readonly float Probability_319;
    public readonly float Probability_320;
    public readonly float Probability_321;
    public readonly float Probability_322;
    public readonly float Probability_323;
    public readonly float Probability_324;
    public readonly float Probability_325;
    public readonly float Probability_326;
    public readonly float Probability_327;
    public readonly float Probability_328;
    public readonly float Probability_329;
    public readonly float Probability_330;
    public readonly float Probability_331;
    public readonly float Probability_332;
    public readonly float Probability_333;
    public readonly float Probability_334;
    public readonly float Probability_335;
    public readonly float Probability_336;
    public readonly float Probability_337;
    public readonly float Probability_338;
    public readonly float Probability_339;
    public readonly float Probability_340;
    public readonly float Probability_341;
    public readonly float Probability_342;
    public readonly float Probability_343;
    public readonly float Probability_344;
    public readonly float Probability_345;
    public readonly float Probability_346;
    public readonly float Probability_347;
    public readonly float Probability_348;
    public readonly float Probability_349;
    public readonly float Probability_350;
    public readonly float Probability_351;
    public readonly float Probability_352;
    public readonly float Probability_353;
    public readonly float Probability_354;
    public readonly float Probability_355;
    public readonly float Probability_356;
    public readonly float Probability_357;
    public readonly float Probability_358;
    public readonly float Probability_359;
    public readonly float Probability_360;
    public readonly float Probability_361;
    public readonly float Probability_362;
    public readonly float Probability_363;
    public readonly float Probability_364;
    public readonly float Probability_365;
    public readonly float Probability_366;
    public readonly float Probability_367;
    public readonly float Probability_368;
    public readonly float Probability_369;
    public readonly float Probability_370;
    public readonly float Probability_371;
    public readonly float Probability_372;
    public readonly float Probability_373;
    public readonly float Probability_374;
    public readonly float Probability_375;
    public readonly float Probability_376;
    public readonly float Probability_377;
    public readonly float Probability_378;
    public readonly float Probability_379;
    public readonly float Probability_380;
    public readonly float Probability_381;
    public readonly float Probability_382;
    public readonly float Probability_383;
    public readonly float Probability_384;
    public readonly float Probability_385;
    public readonly float Probability_386;
    public readonly float Probability_387;
    public readonly float Probability_388;
    public readonly float Probability_389;
    public readonly float Probability_390;
    public readonly float Probability_391;
    public readonly float Probability_392;
    public readonly float Probability_393;
    public readonly float Probability_394;
    public readonly float Probability_395;
    public readonly float Probability_396;
    public readonly float Probability_397;
    public readonly float Probability_398;
    public readonly float Probability_399;
    public readonly float Probability_400;
    public readonly float Probability_401;
    public readonly float Probability_402;
    public readonly float Probability_403;
    public readonly float Probability_404;
    public readonly float Probability_405;
    public readonly float Probability_406;
    public readonly float Probability_407;
    public readonly float Probability_408;
    public readonly float Probability_409;
    public readonly float Probability_410;
    public readonly float Probability_411;
    public readonly float Probability_412;
    public readonly float Probability_413;
    public readonly float Probability_414;
    public readonly float Probability_415;
    public readonly float Probability_416;
    public readonly float Probability_417;
    public readonly float Probability_418;
    public readonly float Probability_419;
    public readonly float Probability_420;
    public readonly float Probability_421;
    public readonly float Probability_422;
    public readonly float Probability_423;
    public readonly float Probability_424;
    public readonly float Probability_425;
    public readonly float Probability_426;
    public readonly float Probability_427;
    public readonly float Probability_428;
    public readonly float Probability_429;
    public readonly float Probability_430;
    public readonly float Probability_431;
    public readonly float Probability_432;
    public readonly float Probability_433;
    public readonly float Probability_434;
    public readonly float Probability_435;
    public readonly float Probability_436;
    public readonly float Probability_437;
    public readonly float Probability_438;
    public readonly float Probability_439;
    public readonly float Probability_440;
    public readonly float Probability_441;
    public readonly float Probability_442;
    public readonly float Probability_443;
    public readonly float Probability_444;
    public readonly float Probability_445;
    public readonly float Probability_446;
    public readonly float Probability_447;
    public readonly float Probability_448;
    public readonly float Probability_449;
    public readonly float Probability_450;
    public readonly float Probability_451;
    public readonly float Probability_452;
    public readonly float Probability_453;
    public readonly float Probability_454;
    public readonly float Probability_455;
    public readonly float Probability_456;
    public readonly float Probability_457;
    public readonly float Probability_458;
    public readonly float Probability_459;
    public readonly float Probability_460;
    public readonly float Probability_461;
    public readonly float Probability_462;
    public readonly float Probability_463;
    public readonly float Probability_464;
    public readonly float Probability_465;
    public readonly float Probability_466;
    public readonly float Probability_467;
    public readonly float Probability_468;
    public readonly float Probability_469;
    public readonly float Probability_470;
    public readonly float Probability_471;
    public readonly float Probability_472;
    public readonly float Probability_473;
    public readonly float Probability_474;
    public readonly float Probability_475;
    public readonly float Probability_476;
    public readonly float Probability_477;
    public readonly float Probability_478;
    public readonly float Probability_479;
    public readonly float Probability_480;
    public readonly float Probability_481;
    public readonly float Probability_482;
    public readonly float Probability_483;
    public readonly float Probability_484;
    public readonly float Probability_485;
    public readonly float Probability_486;
    public readonly float Probability_487;
    public readonly float Probability_488;
    public readonly float Probability_489;
    public readonly float Probability_490;
    public readonly float Probability_491;
    public readonly float Probability_492;
    public readonly float Probability_493;
    public readonly float Probability_494;
    public readonly float Probability_495;
    public readonly float Probability_496;
    public readonly float Probability_497;
    public readonly float Probability_498;
    public readonly float Probability_499;
    public readonly float Probability_500;
    public readonly float Probability_501;
    public readonly float Probability_502;
    public readonly float Probability_503;
    public readonly float Probability_504;
    public readonly float Probability_505;
    public readonly float Probability_506;
    public readonly float Probability_507;
    public readonly float Probability_508;
    public readonly float Probability_509;
    public readonly float Probability_510;
    public readonly float Probability_511;
    public readonly float Probability_512;
    public readonly float Probability_513;
    public readonly float Probability_514;
    public readonly float Probability_515;
    public readonly float Probability_516;
    public readonly float Probability_517;
    public readonly float Probability_518;
    public readonly float Probability_519;
    public readonly float Probability_520;
    public readonly float Probability_521;
    public readonly float Probability_522;
    public readonly float Probability_523;
    public readonly float Probability_524;
    public readonly float Probability_525;
    public readonly float Probability_526;
    public readonly float Probability_527;
    public readonly float Probability_528;
    public readonly float Probability_529;
    public readonly float Probability_530;
    public readonly float Probability_531;
    public readonly float Probability_532;
    public readonly float Probability_533;
    public readonly float Probability_534;
    public readonly float Probability_535;
    public readonly float Probability_536;
    public readonly float Probability_537;
    public readonly float Probability_538;
    public readonly float Probability_539;
    public readonly float Probability_540;
    public readonly float Probability_541;
    public readonly float Probability_542;
    public readonly float Probability_543;
    public readonly float Probability_544;
    public readonly float Probability_545;
    public readonly float Probability_546;
    public readonly float Probability_547;
    public readonly float Probability_548;
    public readonly float Probability_549;
    public readonly float Probability_550;
    public readonly float Probability_551;
    public readonly float Probability_552;
    public readonly float Probability_553;
    public readonly float Probability_554;
    public readonly float Probability_555;
    public readonly float Probability_556;
    public readonly float Probability_557;
    public readonly float Probability_558;
    public readonly float Probability_559;
    public readonly float Probability_560;
    public readonly float Probability_561;
    public readonly float Probability_562;
    public readonly float Probability_563;
    public readonly float Probability_564;
    public readonly float Probability_565;
    public readonly float Probability_566;
    public readonly float Probability_567;
    public readonly float Probability_568;
    public readonly float Probability_569;
    public readonly float Probability_570;
    public readonly float Probability_571;
    public readonly float Probability_572;
    public readonly float Probability_573;
    public readonly float Probability_574;
    public readonly float Probability_575;
    public readonly float Probability_576;
    public readonly float Probability_577;
    public readonly float Probability_578;
    public readonly float Probability_579;
    public readonly float Probability_580;
    public readonly float Probability_581;
    public readonly float Probability_582;
    public readonly float Probability_583;
    public readonly float Probability_584;
    public readonly float Probability_585;
    public readonly float Probability_586;
    public readonly float Probability_587;
    public readonly float Probability_588;
    public readonly float Probability_589;
    public readonly float Probability_590;
    public readonly float Probability_591;
    public readonly float Probability_592;
    public readonly float Probability_593;
    public readonly float Probability_594;
    public readonly float Probability_595;
    public readonly float Probability_596;
    public readonly float Probability_597;
    public readonly float Probability_598;
    public readonly float Probability_599;
    public readonly float Probability_600;
    public readonly float Probability_601;
    public readonly float Probability_602;
    public readonly float Probability_603;
    public readonly float Probability_604;
    public readonly float Probability_605;
    public readonly float Probability_606;
    public readonly float Probability_607;
    public readonly float Probability_608;
    public readonly float Probability_609;
    public readonly float Probability_610;
    public readonly float Probability_611;
    public readonly float Probability_612;
    public readonly float Probability_613;
    public readonly float Probability_614;
    public readonly float Probability_615;
    public readonly float Probability_616;
    public readonly float Probability_617;
    public readonly float Probability_618;
    public readonly float Probability_619;
    public readonly float Probability_620;
    public readonly float Probability_621;
    public readonly float Probability_622;
    public readonly float Probability_623;
    public readonly float Probability_624;
    public readonly float Probability_625;
    public readonly float Probability_626;
    public readonly float Probability_627;
    public readonly float Probability_628;
    public readonly float Probability_629;
    public readonly float Probability_630;
    public readonly float Probability_631;
    public readonly float Probability_632;
    public readonly float Probability_633;
    public readonly float Probability_634;
    public readonly float Probability_635;
    public readonly float Probability_636;
    public readonly float Probability_637;
    public readonly float Probability_638;
    public readonly float Probability_639;
    public readonly float Probability_640;
    public readonly float Probability_641;
    public readonly float Probability_642;
    public readonly float Probability_643;
    public readonly float Probability_644;
    public readonly float Probability_645;
    public readonly float Probability_646;
    public readonly float Probability_647;
    public readonly float Probability_648;
    public readonly float Probability_649;
    public readonly float Probability_650;
    public readonly float Probability_651;
    public readonly float Probability_652;
    public readonly float Probability_653;
    public readonly float Probability_654;
    public readonly float Probability_655;
    public readonly float Probability_656;
    public readonly float Probability_657;
    public readonly float Probability_658;
    public readonly float Probability_659;
    public readonly float Probability_660;
    public readonly float Probability_661;
    public readonly float Probability_662;
    public readonly float Probability_663;
    public readonly float Probability_664;
    public readonly float Probability_665;
    public readonly float Probability_666;
    public readonly float Probability_667;
    public readonly float Probability_668;
    public readonly float Probability_669;
    public readonly float Probability_670;
    public readonly float Probability_671;
    public readonly float Probability_672;
    public readonly float Probability_673;
    public readonly float Probability_674;
    public readonly float Probability_675;
    public readonly float Probability_676;
    public readonly float Probability_677;
    public readonly float Probability_678;
    public readonly float Probability_679;
    public readonly float Probability_680;
    public readonly float Probability_681;
    public readonly float Probability_682;
    public readonly float Probability_683;
    public readonly float Probability_684;
    public readonly float Probability_685;
    public readonly float Probability_686;
    public readonly float Probability_687;
    public readonly float Probability_688;
    public readonly float Probability_689;
    public readonly float Probability_690;
    public readonly float Probability_691;
    public readonly float Probability_692;
    public readonly float Probability_693;
    public readonly float Probability_694;
    public readonly float Probability_695;
    public readonly float Probability_696;
    public readonly float Probability_697;
    public readonly float Probability_698;
    public readonly float Probability_699;
    public readonly float Probability_700;
    public readonly float Probability_701;
    public readonly float Probability_702;
    public readonly float Probability_703;
    public readonly float Probability_704;
    public readonly float Probability_705;
    public readonly float Probability_706;
    public readonly float Probability_707;
    public readonly float Probability_708;
    public readonly float Probability_709;
    public readonly float Probability_710;
    public readonly float Probability_711;
    public readonly float Probability_712;
    public readonly float Probability_713;
    public readonly float Probability_714;
    public readonly float Probability_715;
    public readonly float Probability_716;
    public readonly float Probability_717;
    public readonly float Probability_718;
    public readonly float Probability_719;
    public readonly float Probability_720;
    public readonly float Probability_721;
    public readonly float Probability_722;
    public readonly float Probability_723;
    public readonly float Probability_724;
    public readonly float Probability_725;
    public readonly float Probability_726;
    public readonly float Probability_727;
    public readonly float Probability_728;
    public readonly float Probability_729;
    public readonly float Probability_730;
    public readonly float Probability_731;
    public readonly float Probability_732;
    public readonly float Probability_733;
    public readonly float Probability_734;
    public readonly float Probability_735;
    public readonly float Probability_736;
    public readonly float Probability_737;
    public readonly float Probability_738;
    public readonly float Probability_739;
    public readonly float Probability_740;
    public readonly float Probability_741;
    public readonly float Probability_742;
    public readonly float Probability_743;
    public readonly float Probability_744;
    public readonly float Probability_745;
    public readonly float Probability_746;
    public readonly float Probability_747;
    public readonly float Probability_748;
    public readonly float Probability_749;
    public readonly float Probability_750;
    public readonly float Probability_751;
    public readonly float Probability_752;
    public readonly float Probability_753;
    public readonly float Probability_754;
    public readonly float Probability_755;
    public readonly float Probability_756;
    public readonly float Probability_757;
    public readonly float Probability_758;
    public readonly float Probability_759;
    public readonly float Probability_760;
    public readonly float Probability_761;
    public readonly float Probability_762;
    public readonly float Probability_763;
    public readonly float Probability_764;
    public readonly float Probability_765;
    public readonly float Probability_766;
    public readonly float Probability_767;
    public readonly float Probability_768;
    public readonly float Probability_769;
    public readonly float Probability_770;
    public readonly float Probability_771;
    public readonly float Probability_772;
    public readonly float Probability_773;
    public readonly float Probability_774;
    public readonly float Probability_775;
    public readonly float Probability_776;
    public readonly float Probability_777;
    public readonly float Probability_778;
    public readonly float Probability_779;
    public readonly float Probability_780;
    public readonly float Probability_781;
    public readonly float Probability_782;
    public readonly float Probability_783;
    public readonly float Probability_784;
    public readonly float Probability_785;
    public readonly float Probability_786;
    public readonly float Probability_787;
    public readonly float Probability_788;
    public readonly float Probability_789;
    public readonly float Probability_790;
    public readonly float Probability_791;
    public readonly float Probability_792;
    public readonly float Probability_793;
    public readonly float Probability_794;
    public readonly float Probability_795;
    public readonly float Probability_796;
    public readonly float Probability_797;
    public readonly float Probability_798;
    public readonly float Probability_799;
    public readonly float Probability_800;
    public readonly float Probability_801;
    public readonly float Probability_802;
    public readonly float Probability_803;
    public readonly float Probability_804;
    public readonly float Probability_805;
    public readonly float Probability_806;
    public readonly float Probability_807;
    public readonly float Probability_808;
    public readonly float Probability_809;
    public readonly float Probability_810;
    public readonly float Probability_811;
    public readonly float Probability_812;
    public readonly float Probability_813;
    public readonly float Probability_814;
    public readonly float Probability_815;
    public readonly float Probability_816;
    public readonly float Probability_817;
    public readonly float Probability_818;
    public readonly float Probability_819;
    public readonly float Probability_820;
    public readonly float Probability_821;
    public readonly float Probability_822;
    public readonly float Probability_823;
    public readonly float Probability_824;
    public readonly float Probability_825;
    public readonly float Probability_826;
    public readonly float Probability_827;
    public readonly float Probability_828;
    public readonly float Probability_829;
    public readonly float Probability_830;
    public readonly float Probability_831;
    public readonly float Probability_832;
    public readonly float Probability_833;
    public readonly float Probability_834;
    public readonly float Probability_835;
    public readonly float Probability_836;
    public readonly float Probability_837;
    public readonly float Probability_838;
    public readonly float Probability_839;
    public readonly float Probability_840;
    public readonly float Probability_841;
    public readonly float Probability_842;
    public readonly float Probability_843;
    public readonly float Probability_844;
    public readonly float Probability_845;
    public readonly float Probability_846;
    public readonly float Probability_847;
    public readonly float Probability_848;
    public readonly float Probability_849;
    public readonly float Probability_850;
    public readonly float Probability_851;
    public readonly float Probability_852;
    public readonly float Probability_853;
    public readonly float Probability_854;
    public readonly float Probability_855;
    public readonly float Probability_856;
    public readonly float Probability_857;
    public readonly float Probability_858;
    public readonly float Probability_859;
    public readonly float Probability_860;
    public readonly float Probability_861;
    public readonly float Probability_862;
    public readonly float Probability_863;
    public readonly float Probability_864;
    public readonly float Probability_865;
    public readonly float Probability_866;
    public readonly float Probability_867;
    public readonly float Probability_868;
    public readonly float Probability_869;
    public readonly float Probability_870;
    public readonly float Probability_871;
    public readonly float Probability_872;
    public readonly float Probability_873;
    public readonly float Probability_874;
    public readonly float Probability_875;
    public readonly float Probability_876;
    public readonly float Probability_877;
    public readonly float Probability_878;
    public readonly float Probability_879;
    public readonly float Probability_880;
    public readonly float Probability_881;
    public readonly float Probability_882;
    public readonly float Probability_883;
    public readonly float Probability_884;
    public readonly float Probability_885;
    public readonly float Probability_886;
    public readonly float Probability_887;
    public readonly float Probability_888;
    public readonly float Probability_889;
    public readonly float Probability_890;
    public readonly float Probability_891;
    public readonly float Probability_892;
    public readonly float Probability_893;
    public readonly float Probability_894;
    public readonly float Probability_895;
    public readonly float Probability_896;
    public readonly float Probability_897;
    public readonly float Probability_898;
    public readonly float Probability_899;
    public readonly float Probability_900;
    public readonly float Probability_901;
    public readonly float Probability_902;
    public readonly float Probability_903;
    public readonly float Probability_904;
    public readonly float Probability_905;
    public readonly float Probability_906;
    public readonly float Probability_907;
    public readonly float Probability_908;
    public readonly float Probability_909;
    public readonly float Probability_910;
    public readonly float Probability_911;
    public readonly float Probability_912;
    public readonly float Probability_913;
    public readonly float Probability_914;
    public readonly float Probability_915;
    public readonly float Probability_916;
    public readonly float Probability_917;
    public readonly float Probability_918;
    public readonly float Probability_919;
    public readonly float Probability_920;
    public readonly float Probability_921;
    public readonly float Probability_922;
    public readonly float Probability_923;
    public readonly float Probability_924;
    public readonly float Probability_925;
    public readonly float Probability_926;
    public readonly float Probability_927;
    public readonly float Probability_928;
    public readonly float Probability_929;
    public readonly float Probability_930;
    public readonly float Probability_931;
    public readonly float Probability_932;
    public readonly float Probability_933;
    public readonly float Probability_934;
    public readonly float Probability_935;
    public readonly float Probability_936;
    public readonly float Probability_937;
    public readonly float Probability_938;
    public readonly float Probability_939;
    public readonly float Probability_940;
    public readonly float Probability_941;
    public readonly float Probability_942;
    public readonly float Probability_943;
    public readonly float Probability_944;
    public readonly float Probability_945;
    public readonly float Probability_946;
    public readonly float Probability_947;
    public readonly float Probability_948;
    public readonly float Probability_949;
    public readonly float Probability_950;
    public readonly float Probability_951;
    public readonly float Probability_952;
    public readonly float Probability_953;
    public readonly float Probability_954;
    public readonly float Probability_955;
    public readonly float Probability_956;
    public readonly float Probability_957;
    public readonly float Probability_958;
    public readonly float Probability_959;
    public readonly float Probability_960;
    public readonly float Probability_961;
    public readonly float Probability_962;
    public readonly float Probability_963;
    public readonly float Probability_964;
    public readonly float Probability_965;
    public readonly float Probability_966;
    public readonly float Probability_967;
    public readonly float Probability_968;
    public readonly float Probability_969;
    public readonly float Probability_970;
    public readonly float Probability_971;
    public readonly float Probability_972;
    public readonly float Probability_973;
    public readonly float Probability_974;
    public readonly float Probability_975;
    public readonly float Probability_976;
    public readonly float Probability_977;
    public readonly float Probability_978;
    public readonly float Probability_979;
    public readonly float Probability_980;
    public readonly float Probability_981;
    public readonly float Probability_982;
    public readonly float Probability_983;
    public readonly float Probability_984;
    public readonly float Probability_985;
    public readonly float Probability_986;
    public readonly float Probability_987;
    public readonly float Probability_988;
    public readonly float Probability_989;
    public readonly float Probability_990;
    public readonly float Probability_991;
    public readonly float Probability_992;
    public readonly float Probability_993;
    public readonly float Probability_994;
    public readonly float Probability_995;
    public readonly float Probability_996;
    public readonly float Probability_997;
    public readonly float Probability_998;
    public readonly float Probability_999;
    public readonly float Probability_1000;
    public readonly float Probability_1001;
    public readonly float Probability_1002;
    public readonly float Probability_1003;
    public readonly float Probability_1004;
    public readonly float Probability_1005;
    public readonly float Probability_1006;
    public readonly float Probability_1007;
    public readonly float Probability_1008;
    public readonly float Probability_1009;
    public readonly float Probability_1010;
    public readonly float Probability_1011;
    public readonly float Probability_1012;
    public readonly float Probability_1013;
    public readonly float Probability_1014;
    public readonly float Probability_1015;
    public readonly float Probability_1016;
    public readonly float Probability_1017;
    public readonly float Probability_1018;
    public readonly float Probability_1019;
    public readonly float Probability_1020;
    public readonly float Probability_1021;
    public readonly float Probability_1022;
    public readonly float Probability_1023;
    public readonly float Probability_1024;
    public readonly float Probability_1025;
    public readonly float Probability_1026;
    public readonly float Probability_1027;
    public readonly float Probability_1028;
    public readonly float Probability_1029;
    public readonly float Probability_1030;
    public readonly float Probability_1031;
    public readonly float Probability_1032;
    public readonly float Probability_1033;
    public readonly float Probability_1034;
    public readonly float Probability_1035;
    public readonly float Probability_1036;
    public readonly float Probability_1037;
    public readonly float Probability_1038;
    public readonly float Probability_1039;
    public readonly float Probability_1040;
    public readonly float Probability_1041;
    public readonly float Probability_1042;
    public readonly float Probability_1043;
    public readonly float Probability_1044;
    public readonly float Probability_1045;
    public readonly float Probability_1046;
    public readonly float Probability_1047;
    public readonly float Probability_1048;
    public readonly float Probability_1049;
    public readonly float Probability_1050;
    public readonly float Probability_1051;
    public readonly float Probability_1052;
    public readonly float Probability_1053;
    public readonly float Probability_1054;
    public readonly float Probability_1055;
    public readonly float Probability_1056;
    public readonly float Probability_1057;
    public readonly float Probability_1058;
    public readonly float Probability_1059;
    public readonly float Probability_1060;
    public readonly float Probability_1061;
    public readonly float Probability_1062;
    public readonly float Probability_1063;
    public readonly float Probability_1064;
    public readonly float Probability_1065;
    public readonly float Probability_1066;
    public readonly float Probability_1067;
    public readonly float Probability_1068;
    public readonly float Probability_1069;
    public readonly float Probability_1070;
    public readonly float Probability_1071;
    public readonly float Probability_1072;
    public readonly float Probability_1073;
    public readonly float Probability_1074;
    public readonly float Probability_1075;
    public readonly float Probability_1076;
    public readonly float Probability_1077;
    public readonly float Probability_1078;
    public readonly float Probability_1079;
    public readonly float Probability_1080;
    public readonly float Probability_1081;
    public readonly float Probability_1082;
    public readonly float Probability_1083;
    public readonly float Probability_1084;
    public readonly float Probability_1085;
    public readonly float Probability_1086;
    public readonly float Probability_1087;
    public readonly float Probability_1088;
    public readonly float Probability_1089;
    public readonly float Probability_1090;
    public readonly float Probability_1091;
    public readonly float Probability_1092;
    public readonly float Probability_1093;
    public readonly float Probability_1094;
    public readonly float Probability_1095;
    public readonly float Probability_1096;
    public readonly float Probability_1097;
    public readonly float Probability_1098;
    public readonly float Probability_1099;
    public readonly float Probability_1100;
    public readonly float Probability_1101;
    public readonly float Probability_1102;
    public readonly float Probability_1103;
    public readonly float Probability_1104;
    public readonly float Probability_1105;
    public readonly float Probability_1106;
    public readonly float Probability_1107;
    public readonly float Probability_1108;
    public readonly float Probability_1109;
    public readonly float Probability_1110;
    public readonly float Probability_1111;
    public readonly float Probability_1112;
    public readonly float Probability_1113;
    public readonly float Probability_1114;
    public readonly float Probability_1115;
    public readonly float Probability_1116;
    public readonly float Probability_1117;
    public readonly float Probability_1118;
    public readonly float Probability_1119;
    public readonly float Probability_1120;
    public readonly float Probability_1121;
    public readonly float Probability_1122;
    public readonly float Probability_1123;
    public readonly float Probability_1124;
    public readonly float Probability_1125;
    public readonly float Probability_1126;
    public readonly float Probability_1127;
    public readonly float Probability_1128;
    public readonly float Probability_1129;
    public readonly float Probability_1130;
    public readonly float Probability_1131;
    public readonly float Probability_1132;
    public readonly float Probability_1133;
    public readonly float Probability_1134;
    public readonly float Probability_1135;
    public readonly float Probability_1136;
    public readonly float Probability_1137;
    public readonly float Probability_1138;
    public readonly float Probability_1139;
    public readonly float Probability_1140;
    public readonly float Probability_1141;
    public readonly float Probability_1142;
    public readonly float Probability_1143;
    public readonly float Probability_1144;
    public readonly float Probability_1145;
    public readonly float Probability_1146;
    public readonly float Probability_1147;
    public readonly float Probability_1148;
    public readonly float Probability_1149;
    public readonly float Probability_1150;
    public readonly float Probability_1151;
    public readonly float Probability_1152;
    public readonly float Probability_1153;
    public readonly float Probability_1154;
    public readonly float Probability_1155;
    public readonly float Probability_1156;
    public readonly float Probability_1157;
    public readonly float Probability_1158;
    public readonly float Probability_1159;
    public readonly float Probability_1160;
    public readonly float Probability_1161;
    public readonly float Probability_1162;
    public readonly float Probability_1163;
    public readonly float Probability_1164;
    public readonly float Probability_1165;
    public readonly float Probability_1166;
    public readonly float Probability_1167;
    public readonly float Probability_1168;
    public readonly float Probability_1169;
    public readonly float Probability_1170;
    public readonly float Probability_1171;
    public readonly float Probability_1172;
    public readonly float Probability_1173;
    public readonly float Probability_1174;
    public readonly float Probability_1175;
    public readonly float Probability_1176;
    public readonly float Probability_1177;
    public readonly float Probability_1178;
    public readonly float Probability_1179;
    public readonly float Probability_1180;
    public readonly float Probability_1181;
    public readonly float Probability_1182;
    public readonly float Probability_1183;
    public readonly float Probability_1184;
    public readonly float Probability_1185;
    public readonly float Probability_1186;
    public readonly float Probability_1187;
    public readonly float Probability_1188;
    public readonly float Probability_1189;
    public readonly float Probability_1190;
    public readonly float Probability_1191;
    public readonly float Probability_1192;
    public readonly float Probability_1193;
    public readonly float Probability_1194;
    public readonly float Probability_1195;
    public readonly float Probability_1196;
    public readonly float Probability_1197;
    public readonly float Probability_1198;
    public readonly float Probability_1199;
    public readonly float Probability_1200;
    public readonly float Probability_1201;
    public readonly float Probability_1202;
    public readonly float Probability_1203;
    public readonly float Probability_1204;
    public readonly float Probability_1205;
    public readonly float Probability_1206;
    public readonly float Probability_1207;
    public readonly float Probability_1208;
    public readonly float Probability_1209;
    public readonly float Probability_1210;
    public readonly float Probability_1211;
    public readonly float Probability_1212;
    public readonly float Probability_1213;
    public readonly float Probability_1214;
    public readonly float Probability_1215;
    public readonly float Probability_1216;
    public readonly float Probability_1217;
    public readonly float Probability_1218;
    public readonly float Probability_1219;
    public readonly float Probability_1220;
    public readonly float Probability_1221;
    public readonly float Probability_1222;
    public readonly float Probability_1223;
    public readonly float Probability_1224;
    public readonly float Probability_1225;
    public readonly float Probability_1226;
    public readonly float Probability_1227;
    public readonly float Probability_1228;
    public readonly float Probability_1229;
    public readonly float Probability_1230;
    public readonly float Probability_1231;
    public readonly float Probability_1232;
    public readonly float Probability_1233;
    public readonly float Probability_1234;
    public readonly float Probability_1235;
    public readonly float Probability_1236;
    public readonly float Probability_1237;
    public readonly float Probability_1238;
    public readonly float Probability_1239;
    public readonly float Probability_1240;
    public readonly float Probability_1241;
    public readonly float Probability_1242;
    public readonly float Probability_1243;
    public readonly float Probability_1244;
    public readonly float Probability_1245;
    public readonly float Probability_1246;
    public readonly float Probability_1247;
    public readonly float Probability_1248;
    public readonly float Probability_1249;
    public readonly float Probability_1250;
    public readonly float Probability_1251;
    public readonly float Probability_1252;
    public readonly float Probability_1253;
    public readonly float Probability_1254;
    public readonly float Probability_1255;
    public readonly float Probability_1256;
    public readonly float Probability_1257;
    public readonly float Probability_1258;
    public readonly float Probability_1259;
    public readonly float Probability_1260;
    public readonly float Probability_1261;
    public readonly float Probability_1262;
    public readonly float Probability_1263;
    public readonly float Probability_1264;
    public readonly float Probability_1265;
    public readonly float Probability_1266;
    public readonly float Probability_1267;
    public readonly float Probability_1268;
    public readonly float Probability_1269;
    public readonly float Probability_1270;
    public readonly float Probability_1271;
    public readonly float Probability_1272;
    public readonly float Probability_1273;
    public readonly float Probability_1274;
    public readonly float Probability_1275;
    public readonly float Probability_1276;
    public readonly float Probability_1277;
    public readonly float Probability_1278;
    public readonly float Probability_1279;
    public readonly float Probability_1280;
    public readonly float Probability_1281;
    public readonly float Probability_1282;
    public readonly float Probability_1283;
    public readonly float Probability_1284;
    public readonly float Probability_1285;
    public readonly float Probability_1286;
    public readonly float Probability_1287;
    public readonly float Probability_1288;
    public readonly float Probability_1289;
    public readonly float Probability_1290;
    public readonly float Probability_1291;
    public readonly float Probability_1292;
    public readonly float Probability_1293;
    public readonly float Probability_1294;
    public readonly float Probability_1295;
    public readonly float Probability_1296;
    public readonly float Probability_1297;
    public readonly float Probability_1298;
    public readonly float Probability_1299;
    public readonly float Probability_1300;
    public readonly float Probability_1301;
    public readonly float Probability_1302;
    public readonly float Probability_1303;
    public readonly float Probability_1304;
    public readonly float Probability_1305;
    public readonly float Probability_1306;
    public readonly float Probability_1307;
    public readonly float Probability_1308;
    public readonly float Probability_1309;
    public readonly float Probability_1310;
    public readonly float Probability_1311;
    public readonly float Probability_1312;
    public readonly float Probability_1313;
    public readonly float Probability_1314;
    public readonly float Probability_1315;
    public readonly float Probability_1316;
    public readonly float Probability_1317;
    public readonly float Probability_1318;
    public readonly float Probability_1319;
    public readonly float Probability_1320;
    public readonly float Probability_1321;
    public readonly float Probability_1322;
    public readonly float Probability_1323;
    public readonly float Probability_1324;
    public readonly float Probability_1325;
    public readonly float Probability_1326;
    public readonly float Probability_1327;
    public readonly float Probability_1328;
    public readonly float Probability_1329;
    public readonly float Probability_1330;
    public readonly float Probability_1331;
    public readonly float Probability_1332;
    public readonly float Probability_1333;
    public readonly float Probability_1334;
    public readonly float Probability_1335;
    public readonly float Probability_1336;
    public readonly float Probability_1337;
    public readonly float Probability_1338;
    public readonly float Probability_1339;
    public readonly float Probability_1340;
    public readonly float Probability_1341;
    public readonly float Probability_1342;
    public readonly float Probability_1343;
    public readonly float Probability_1344;
    public readonly float Probability_1345;
    public readonly float Probability_1346;
    public readonly float Probability_1347;
    public readonly float Probability_1348;
    public readonly float Probability_1349;
    public readonly float Probability_1350;
    public readonly float Probability_1351;
    public readonly float Probability_1352;
    public readonly float Probability_1353;
    public readonly float Probability_1354;
    public readonly float Probability_1355;
    public readonly float Probability_1356;
    public readonly float Probability_1357;
    public readonly float Probability_1358;
    public readonly float Probability_1359;
    public readonly float Probability_1360;
    public readonly float Probability_1361;
    public readonly float Probability_1362;
    public readonly float Probability_1363;
    public readonly float Probability_1364;
    public readonly float Probability_1365;
    public readonly float Probability_1366;
    public readonly float Probability_1367;
    public readonly float Probability_1368;
    public readonly float Probability_1369;
    public readonly float Probability_1370;
    public readonly float Probability_1371;
    public readonly float Probability_1372;
    public readonly float Probability_1373;
    public readonly float Probability_1374;
    public readonly float Probability_1375;
    public readonly float Probability_1376;
    public readonly float Probability_1377;
    public readonly float Probability_1378;
    public readonly float Probability_1379;
    public readonly float Probability_1380;
    public readonly float Probability_1381;
    public readonly float Probability_1382;
    public readonly float Probability_1383;
    public readonly float Probability_1384;
    public readonly float Probability_1385;
    public readonly float Probability_1386;
    public readonly float Probability_1387;
    public readonly float Probability_1388;
    public readonly float Probability_1389;
    public readonly float Probability_1390;
    public readonly float Probability_1391;
    public readonly float Probability_1392;
    public readonly float Probability_1393;
    public readonly float Probability_1394;
    public readonly float Probability_1395;
    public readonly float Probability_1396;
    public readonly float Probability_1397;
    public readonly float Probability_1398;
    public readonly float Probability_1399;
    public readonly float Probability_1400;
    public readonly float Probability_1401;
    public readonly float Probability_1402;
    public readonly float Probability_1403;
    public readonly float Probability_1404;
    public readonly float Probability_1405;
    public readonly float Probability_1406;
    public readonly float Probability_1407;
    public readonly float Probability_1408;
    public readonly float Probability_1409;
    public readonly float Probability_1410;
    public readonly float Probability_1411;
    public readonly float Probability_1412;
    public readonly float Probability_1413;
    public readonly float Probability_1414;
    public readonly float Probability_1415;
    public readonly float Probability_1416;
    public readonly float Probability_1417;
    public readonly float Probability_1418;
    public readonly float Probability_1419;
    public readonly float Probability_1420;
    public readonly float Probability_1421;
    public readonly float Probability_1422;
    public readonly float Probability_1423;
    public readonly float Probability_1424;
    public readonly float Probability_1425;
    public readonly float Probability_1426;
    public readonly float Probability_1427;
    public readonly float Probability_1428;
    public readonly float Probability_1429;
    public readonly float Probability_1430;
    public readonly float Probability_1431;
    public readonly float Probability_1432;
    public readonly float Probability_1433;
    public readonly float Probability_1434;
    public readonly float Probability_1435;
    public readonly float Probability_1436;
    public readonly float Probability_1437;
    public readonly float Probability_1438;
    public readonly float Probability_1439;
    public readonly float Probability_1440;
    public readonly float Probability_1441;
    public readonly float Probability_1442;
    public readonly float Probability_1443;
    public readonly float Probability_1444;
    public readonly float Probability_1445;
    public readonly float Probability_1446;
    public readonly float Probability_1447;
    public readonly float Probability_1448;
    public readonly float Probability_1449;
    public readonly float Probability_1450;
    public readonly float Probability_1451;
    public readonly float Probability_1452;
    public readonly float Probability_1453;
    public readonly float Probability_1454;
    public readonly float Probability_1455;
    public readonly float Probability_1456;
    public readonly float Probability_1457;
    public readonly float Probability_1458;
    public readonly float Probability_1459;
    public readonly float Probability_1460;
    public readonly float Probability_1461;
    public readonly float Probability_1462;
    public readonly float Probability_1463;
    public readonly float Probability_1464;
    public readonly float Probability_1465;
    public readonly float Probability_1466;
    public readonly float Probability_1467;
    public readonly float Probability_1468;
    public readonly float Probability_1469;
    public readonly float Probability_1470;
    public readonly float Probability_1471;
    public readonly float Probability_1472;
    public readonly float Probability_1473;
    public readonly float Probability_1474;
    public readonly float Probability_1475;
    public readonly float Probability_1476;
    public readonly float Probability_1477;
    public readonly float Probability_1478;
    public readonly float Probability_1479;
    public readonly float Probability_1480;
    public readonly float Probability_1481;
    public readonly float Probability_1482;
    public readonly float Probability_1483;
    public readonly float Probability_1484;
    public readonly float Probability_1485;
    public readonly float Probability_1486;
    public readonly float Probability_1487;
    public readonly float Probability_1488;
    public readonly float Probability_1489;
    public readonly float Probability_1490;
    public readonly float Probability_1491;
    public readonly float Probability_1492;
    public readonly float Probability_1493;
    public readonly float Probability_1494;
    public readonly float Probability_1495;
    public readonly float Probability_1496;
    public readonly float Probability_1497;
    public readonly float Probability_1498;
    public readonly float Probability_1499;
    public readonly float Probability_1500;
    public readonly float Probability_1501;
    public readonly float Probability_1502;
    public readonly float Probability_1503;
    public readonly float Probability_1504;
    public readonly float Probability_1505;
    public readonly float Probability_1506;
    public readonly float Probability_1507;
    public readonly float Probability_1508;
    public readonly float Probability_1509;
    public readonly float Probability_1510;
    public readonly float Probability_1511;
    public readonly float Probability_1512;
    public readonly float Probability_1513;
    public readonly float Probability_1514;
    public readonly float Probability_1515;
    public readonly float Probability_1516;
    public readonly float Probability_1517;
    public readonly float Probability_1518;
    public readonly float Probability_1519;
    public readonly float Probability_1520;
    public readonly float Probability_1521;
    public readonly float Probability_1522;
    public readonly float Probability_1523;
    public readonly float Probability_1524;
    public readonly float Probability_1525;
    public readonly float Probability_1526;
    public readonly float Probability_1527;
    public readonly float Probability_1528;
    public readonly float Probability_1529;
    public readonly float Probability_1530;
    public readonly float Probability_1531;
    public readonly float Probability_1532;
    public readonly float Probability_1533;
    public readonly float Probability_1534;
    public readonly float Probability_1535;
    public readonly float Probability_1536;
    public readonly float Probability_1537;
    public readonly float Probability_1538;
    public readonly float Probability_1539;
    public readonly float Probability_1540;
    public readonly float Probability_1541;
    public readonly float Probability_1542;
    public readonly float Probability_1543;
    public readonly float Probability_1544;
    public readonly float Probability_1545;
    public readonly float Probability_1546;
    public readonly float Probability_1547;
    public readonly float Probability_1548;
    public readonly float Probability_1549;
    public readonly float Probability_1550;
    public readonly float Probability_1551;
    public readonly float Probability_1552;
    public readonly float Probability_1553;
    public readonly float Probability_1554;
    public readonly float Probability_1555;
    public readonly float Probability_1556;
    public readonly float Probability_1557;
    public readonly float Probability_1558;
    public readonly float Probability_1559;
    public readonly float Probability_1560;
    public readonly float Probability_1561;
    public readonly float Probability_1562;
    public readonly float Probability_1563;
    public readonly float Probability_1564;
    public readonly float Probability_1565;
    public readonly float Probability_1566;
    public readonly float Probability_1567;
    public readonly float Probability_1568;
    public readonly float Probability_1569;
    public readonly float Probability_1570;
    public readonly float Probability_1571;
    public readonly float Probability_1572;
    public readonly float Probability_1573;
    public readonly float Probability_1574;
    public readonly float Probability_1575;
    public readonly float Probability_1576;
    public readonly float Probability_1577;
    public readonly float Probability_1578;
    public readonly float Probability_1579;
    public readonly float Probability_1580;
    public readonly float Probability_1581;
    public readonly float Probability_1582;
    public readonly float Probability_1583;
    public readonly float Probability_1584;
    public readonly float Probability_1585;
    public readonly float Probability_1586;
    public readonly float Probability_1587;
    public readonly float Probability_1588;
    public readonly float Probability_1589;
    public readonly float Probability_1590;
    public readonly float Probability_1591;
    public readonly float Probability_1592;
    public readonly float Probability_1593;
    public readonly float Probability_1594;
    public readonly float Probability_1595;
    public readonly float Probability_1596;
    public readonly float Probability_1597;
    public readonly float Probability_1598;
    public readonly float Probability_1599;
    public readonly float Probability_1600;
    public readonly float Probability_1601;
    public readonly float Probability_1602;
    public readonly float Probability_1603;
    public readonly float Probability_1604;
    public readonly float Probability_1605;
    public readonly float Probability_1606;
    public readonly float Probability_1607;
    public readonly float Probability_1608;
    public readonly float Probability_1609;
    public readonly float Probability_1610;
    public readonly float Probability_1611;
    public readonly float Probability_1612;
    public readonly float Probability_1613;
    public readonly float Probability_1614;
    public readonly float Probability_1615;
    public readonly float Probability_1616;
    public readonly float Probability_1617;
    public readonly float Probability_1618;
    public readonly float Probability_1619;
    public readonly float Probability_1620;
    public readonly float Probability_1621;
    public readonly float Probability_1622;
    public readonly float Probability_1623;
    public readonly float Probability_1624;
    public readonly float Probability_1625;
    public readonly float Probability_1626;
    public readonly float Probability_1627;
    public readonly float Probability_1628;
    public readonly float Probability_1629;
    public readonly float Probability_1630;
    public readonly float Probability_1631;
    public readonly float Probability_1632;
    public readonly float Probability_1633;
    public readonly float Probability_1634;
    public readonly float Probability_1635;
    public readonly float Probability_1636;
    public readonly float Probability_1637;
    public readonly float Probability_1638;
    public readonly float Probability_1639;
    public readonly float Probability_1640;
    public readonly float Probability_1641;
    public readonly float Probability_1642;
    public readonly float Probability_1643;
    public readonly float Probability_1644;
    public readonly float Probability_1645;
    public readonly float Probability_1646;
    public readonly float Probability_1647;
    public readonly float Probability_1648;
    public readonly float Probability_1649;
    public readonly float Probability_1650;
    public readonly float Probability_1651;
    public readonly float Probability_1652;
    public readonly float Probability_1653;
    public readonly float Probability_1654;
    public readonly float Probability_1655;
    public readonly float Probability_1656;
    public readonly float Probability_1657;
    public readonly float Probability_1658;
    public readonly float Probability_1659;
    public readonly float Probability_1660;
    public readonly float Probability_1661;
    public readonly float Probability_1662;
    public readonly float Probability_1663;
    public readonly float Probability_1664;
    public readonly float Probability_1665;
    public readonly float Probability_1666;
    public readonly float Probability_1667;
    public readonly float Probability_1668;
    public readonly float Probability_1669;
    public readonly float Probability_1670;
    public readonly float Probability_1671;
    public readonly float Probability_1672;
    public readonly float Probability_1673;
    public readonly float Probability_1674;
    public readonly float Probability_1675;
    public readonly float Probability_1676;
    public readonly float Probability_1677;
    public readonly float Probability_1678;
    public readonly float Probability_1679;
    public readonly float Probability_1680;
    public readonly float Probability_1681;
    public readonly float Probability_1682;
    public readonly float Probability_1683;
    public readonly float Probability_1684;
    public readonly float Probability_1685;
    public readonly float Probability_1686;
    public readonly float Probability_1687;
    public readonly float Probability_1688;
    public readonly float Probability_1689;
    public readonly float Probability_1690;
    public readonly float Probability_1691;
    public readonly float Probability_1692;
    public readonly float Probability_1693;
    public readonly float Probability_1694;
    public readonly float Probability_1695;
    public readonly float Probability_1696;
    public readonly float Probability_1697;
    public readonly float Probability_1698;
    public readonly float Probability_1699;
    public readonly float Probability_1700;
    public readonly float Probability_1701;
    public readonly float Probability_1702;
    public readonly float Probability_1703;
    public readonly float Probability_1704;
    public readonly float Probability_1705;
    public readonly float Probability_1706;
    public readonly float Probability_1707;
    public readonly float Probability_1708;
    public readonly float Probability_1709;
    public readonly float Probability_1710;
    public readonly float Probability_1711;
    public readonly float Probability_1712;
    public readonly float Probability_1713;
    public readonly float Probability_1714;
    public readonly float Probability_1715;
    public readonly float Probability_1716;
    public readonly float Probability_1717;
    public readonly float Probability_1718;
    public readonly float Probability_1719;
    public readonly float Probability_1720;
    public readonly float Probability_1721;
    public readonly float Probability_1722;
    public readonly float Probability_1723;
    public readonly float Probability_1724;
    public readonly float Probability_1725;
    public readonly float Probability_1726;
    public readonly float Probability_1727;
    public readonly float Probability_1728;
    public readonly float Probability_1729;
    public readonly float Probability_1730;
    public readonly float Probability_1731;
    public readonly float Probability_1732;
    public readonly float Probability_1733;
    public readonly float Probability_1734;
    public readonly float Probability_1735;
    public readonly float Probability_1736;
    public readonly float Probability_1737;
    public readonly float Probability_1738;
    public readonly float Probability_1739;
    public readonly float Probability_1740;
    public readonly float Probability_1741;
    public readonly float Probability_1742;
    public readonly float Probability_1743;
    public readonly float Probability_1744;
    public readonly float Probability_1745;
    public readonly float Probability_1746;
    public readonly float Probability_1747;
    public readonly float Probability_1748;
    public readonly float Probability_1749;
    public readonly float Probability_1750;
    public readonly float Probability_1751;
    public readonly float Probability_1752;
    public readonly float Probability_1753;
    public readonly float Probability_1754;
    public readonly float Probability_1755;
    public readonly float Probability_1756;
    public readonly float Probability_1757;
    public readonly float Probability_1758;
    public readonly float Probability_1759;
    public readonly float Probability_1760;
    public readonly float Probability_1761;
    public readonly float Probability_1762;
    public readonly float Probability_1763;
    public readonly float Probability_1764;
    public readonly float Probability_1765;
    public readonly float Probability_1766;
    public readonly float Probability_1767;
    public readonly float Probability_1768;
    public readonly float Probability_1769;
    public readonly float Probability_1770;
    public readonly float Probability_1771;
    public readonly float Probability_1772;
    public readonly float Probability_1773;
    public readonly float Probability_1774;
    public readonly float Probability_1775;
    public readonly float Probability_1776;
    public readonly float Probability_1777;
    public readonly float Probability_1778;
    public readonly float Probability_1779;
    public readonly float Probability_1780;
    public readonly float Probability_1781;
    public readonly float Probability_1782;
    public readonly float Probability_1783;
    public readonly float Probability_1784;
    public readonly float Probability_1785;
    public readonly float Probability_1786;
    public readonly float Probability_1787;
    public readonly float Probability_1788;
    public readonly float Probability_1789;
    public readonly float Probability_1790;
    public readonly float Probability_1791;
    public readonly float Probability_1792;
    public readonly float Probability_1793;
    public readonly float Probability_1794;
    public readonly float Probability_1795;
    public readonly float Probability_1796;
    public readonly float Probability_1797;
    public readonly float Probability_1798;
    public readonly float Probability_1799;
    public readonly float Probability_1800;
    public readonly float Probability_1801;
    public readonly float Probability_1802;
    public readonly float Probability_1803;
    public readonly float Probability_1804;
    public readonly float Probability_1805;
    public readonly float Probability_1806;
    public readonly float Probability_1807;
    public readonly float Probability_1808;
    public readonly float Probability_1809;
    public readonly float Probability_1810;
    public readonly float Probability_1811;
    public readonly float Probability_1812;
    public readonly float Probability_1813;
    public readonly float Probability_1814;
    public readonly float Probability_1815;
    public readonly float Probability_1816;
    public readonly float Probability_1817;
    public readonly float Probability_1818;
    public readonly float Probability_1819;
    public readonly float Probability_1820;
    public readonly float Probability_1821;
    public readonly float Probability_1822;
    public readonly float Probability_1823;
    public readonly float Probability_1824;
    public readonly float Probability_1825;
    public readonly float Probability_1826;
    public readonly float Probability_1827;
    public readonly float Probability_1828;
    public readonly float Probability_1829;
    public readonly float Probability_1830;
    public readonly float Probability_1831;
    public readonly float Probability_1832;
    public readonly float Probability_1833;
    public readonly float Probability_1834;
    public readonly float Probability_1835;
    public readonly float Probability_1836;
    public readonly float Probability_1837;
    public readonly float Probability_1838;
    public readonly float Probability_1839;
    public readonly float Probability_1840;
    public readonly float Probability_1841;
    public readonly float Probability_1842;
    public readonly float Probability_1843;
    public readonly float Probability_1844;
    public readonly float Probability_1845;
    public readonly float Probability_1846;
    public readonly float Probability_1847;
    public readonly float Probability_1848;
    public readonly float Probability_1849;
    public readonly float Probability_1850;
    public readonly float Probability_1851;
    public readonly float Probability_1852;
    public readonly float Probability_1853;
    public readonly float Probability_1854;
    public readonly float Probability_1855;
    public readonly float Probability_1856;
    public readonly float Probability_1857;

    #endregion

    /// <summary>
    /// Returns pointer to begining of array of probabilities.
    /// </summary>
    public float* ProbabilitiesPtr
    {
      get
      {
        unsafe
        {
          fixed (float* f = &Probability_0)
          {
            return f;
          }
        }
      }
    }

    /// <summary>
    /// Returns a view 
    /// </summary>
    public unsafe Span<float> ProbabilitiesSpan => MemoryMarshal.CreateSpan(ref ProbabilitiesPtr[0], POLICY_VECTOR_LENGTH);


    /// <summary>
    /// Returns the Encoded move and associated policy for the move at a specified index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public (EncodedMove move, float probability) this[int index]
         =>  (EncodedMove.FromNeuralNetIndex(index), ProbabilitiesPtr[index]);


    /// <summary>
    /// Initializes all entries to a value of -1.
    /// </summary>
    public void InitilializeAllNegativeOne()
    {
      ProbabilitiesSpan.Fill(-1);
    }


    /// <summary>
    /// Returns the sum of all probabilities.
    /// </summary>
    internal float SumProbabilites
    {
      get
      {
        float acc = 0.0f;
        for (int i = 0; i < POLICY_VECTOR_LENGTH; i++)
        {
          float value = ProbabilitiesPtr[i];
          if (value > 0)
          {
            acc += value;
          }
        }

        return acc;
      }
    }


    /// <summary>
    /// Extracts the policy moves and probabilities into the provided spans.
    /// </summary>
    /// <param name="policyMoves"></param>
    /// <param name="policyProbs"></param>
    /// <returns></returns>
    public int ExtractIntoSpans(Span<EncodedMove> policyMoves, Span<float> policyProbs)
    {      
      int policyLen = 0;
      for (short ix = 0; ix < POLICY_VECTOR_LENGTH; ix++)
      {
        (EncodedMove move, float probability) tt = this[ix];
        {
          if (!float.IsNaN(tt.probability) && tt.probability > 0)
          {
            policyMoves[policyLen] = tt.move;
            policyProbs[policyLen] = tt.probability;
            policyLen++;
          }
        }
      }
      return policyLen;
    }



    /// <summary>
    /// Dumps all nonzero policy vector entries to the Console.
    /// </summary>
    public void DumpProb()
    {
      for (int i = 0; i < EncodedPolicyVector.POLICY_VECTOR_LENGTH; i++)
      {
        if (ProbabilitiesPtr[i] > 0)
        {
          Console.WriteLine(i + " " + ProbabilitiesPtr[i]);
        }
      }
    }

    #region Overrides

    public override int GetHashCode()
    {
      throw new NotImplementedException();
    }


    public override bool Equals(object obj)
    {
      if (obj is EncodedPolicyVector)
        return Equals((EncodedPolicyVector)obj);
      else
        return false;
    }

    public bool Equals(EncodedPolicyVector other)
    {
      return base.Equals(other); // rely on base class to do bit comparison
    }


    /// <summary>
    /// Returns array of probabiliites.
    /// </summary>
    public float[] ProbabilitiesWithNegativeOneZeroed
    {
      get
      {
        float* probs = ProbabilitiesPtr;
        float[] ret = new float[POLICY_VECTOR_LENGTH];
        for (short i = 0; i < POLICY_VECTOR_LENGTH; i++)
        {
          float value = probs[i];
         
          if (value != -1)
          {
            ret[i] = value;
          }
        }
        return ret;
      }
    }


    /// <summary>
    /// Enumerates over all (nonzero) probabilities.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<(EncodedMove move, float probability, short index)> Enumerator()
    {
      for (short i = 0; i < POLICY_VECTOR_LENGTH; i++)
      {
        (EncodedMove move, float probability) tt = this[i];
        {
          if (!float.IsNaN(tt.probability) && tt.probability > 0)
            yield return (tt.move, tt.probability, i);
        }
      }
    }


    /// <summary>
    /// Returns the move having highest probability.
    /// </summary>
    public EncodedMove BestMove
    {
      get
      {
        float bestProb = 0;
        int bestMoveIndex = 0;
        Span<float> probabilities = ProbabilitiesSpan;
        for (int i = 0; i < POLICY_VECTOR_LENGTH; i++)
        {
          float probability = probabilities[i];

          if (probability > bestProb && !float.IsNaN(probability))
          {
            bestProb = probability;
            bestMoveIndex = i;
          }
        }

        return EncodedMove.FromNeuralNetIndex(bestMoveIndex);
      }
    }

    #endregion


    #region Validation

    /// <summary>
    /// Checks policy vector for validity, returning a copy of the probabilities with -1 values zeroed.
    /// </summary>
    /// <param name="desc"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal readonly float[] CheckPolicyValidity(string desc)
    {
      float[] probs = ProbabilitiesWithNegativeOneZeroed;
      float sumPolicy = 0;
      for (int i = 0; i < POLICY_VECTOR_LENGTH; i++)
      {
        float policy = probs[i];
        if (policy == -1)
        {
          // Invalid policies may be represented as -1
          policy = 0;
        }

        sumPolicy += policy;

        if (float.IsNaN(policy) || policy > 1.01 || policy < 0)
        {
          throw new Exception("Policy invalid " + policy + " " + desc);
        }
      }

      if (sumPolicy < 0.99f || sumPolicy > 1.01f)
      {
        throw new Exception("Sum policy not 1 (" + sumPolicy + ") " + desc);
      }

      return probs;
    }

    #endregion
  }
}

