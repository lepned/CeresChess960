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
using System.Linq;
using System.Runtime.CompilerServices;
using Ceres.Chess.MoveGen.Converters;
using Ceres.Chess.Textual;

#endregion

namespace Ceres.Chess.MoveGen
{
  /// <summary>
  /// Static helper methods to convert from string to MGMove.
  /// </summary>
  public static class MGMoveFromString
  {
    /// <summary>
    /// Parses a move string from a specified starting position
    /// (either algebraic or SAN format is accepted).
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="moveStr"></param>
    /// <returns></returns>
    public static MGMove ParseMove(MGPosition pos, string moveStr)
    {
      //if (moveStr == "e8g8")
      //{

      //}
      if (!TryParseMoveCoordinateOrAlgebraic(pos, moveStr, out MGMove move))
      {
        Position position = MGChessPositionConverter.PositionFromMGChessPosition(in pos);
        PositionWithMove mfp = SANParser.FromSAN(moveStr, in position);
        return MGMoveConverter.MGMoveFromPosAndMove(in position, mfp.Move);
      }
      else
        return move;
    }

    /// <summary>
    /// Attempts to parse a move string in coordinate or long algebraic format.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="moveStr"></param>
    /// <param name="move"></param>
    /// <returns></returns>
    private static bool TryParseMoveCoordinateOrAlgebraic(MGPosition pos, string moveStr, out MGMove move)
    {
      moveStr = moveStr.ToLower();

      // Sometimes promotions to Knight use the "k" instead of expected "n"
      if (moveStr.EndsWith("k"))
        moveStr = moveStr.Substring(0, moveStr.Length - 1) + "n";

      MGMoveList moves = new MGMoveList();
      MGMoveGen.GenerateMoves(in pos, moves);
      char rank1 = moveStr[1];
      char rank2 = moveStr[3];
      var sameRank = rank1 == rank2;
      if (moveStr == "e8g8")
      {
      }
      foreach (MGMove moveTry in moves.MovesArray)
      {
        if (moveTry.IsCastle && sameRank)
        { 
          var upper = moveStr.ToUpper();
          string fromTo = $"{moveTry.FromSquare}{moveTry.ToSquare}";
 
          if (upper == fromTo)
          {
            move = moveTry;
            return true;
          }
          //test normal chess
         else if ((moveTry.CastleShort && upper == "E8G8") || (moveTry.CastleShort && upper == "E1G1") )
          {
            move = moveTry;
            return true;
          }
          else if ((moveTry.CastleLong && upper == "E8C8") || (moveTry.CastleLong && upper == "E1C1"))
          {
            move = moveTry;
            return true;
          }

        }
        // Accept moves in any of multiple formats, including Chess 960 (for castling variation)
        if (String.Equals(moveTry.MoveStr(MGMoveNotationStyle.LC0Coordinate), moveStr, StringComparison.OrdinalIgnoreCase)
         || String.Equals(moveTry.MoveStr(MGMoveNotationStyle.LC0Coordinate960Format), moveStr, StringComparison.OrdinalIgnoreCase)
         || String.Equals(moveTry.MoveStr(MGMoveNotationStyle.LongAlgebraic), moveStr, StringComparison.OrdinalIgnoreCase))
        {
          move = moveTry;
          return true;
        }
      }

      move = default;
      return false;
    }
  }


}
