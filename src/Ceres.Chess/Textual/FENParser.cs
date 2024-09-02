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
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using Ceres.Base.Misc;
using Ceres.Chess.MoveGen;
using Ceres.Chess.MoveGen.Converters;
using static Ceres.Chess.PieceType;
using static Ceres.Chess.SideType;

#endregion

namespace Ceres.Chess.Textual
{
  /// <summary>
  /// Parser which converts chess positions represented 
  /// as FEN string into FENParseResult
  /// </summary>
  public static partial class FENParser
  {
    /// <summary>
    /// FEN string corresponding to the starting position in chess.
    /// </summary>
    public const string StartFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";


    /// <summary>
    /// Static factory method to create a FENParseResult from a FEN string.
    /// 
    /// NOTE: performance could be improved by passing in the Piece[] preallocated
    /// </summary>
    /// <param name="fen"></param>
    /// <param name="repetitionCount"></param>
    /// <returns></returns>
    public static FENParseResult ParseFEN(string fen, int repetitionCount = 0)
    {
      try
      {
        return DoParseFEN(fen, repetitionCount);
      }
      catch (Exception exc)
      {
        throw new Exception($"Unable to parse the FEN: {fen}");
      }
    }

    public static int CharToNumber(char c, bool white)
    {
      char lowerCase = char.ToLower(c);
      char baseChar = 'a';
      int file = 7 - (lowerCase - baseChar);

      if (white)
      {
        return file;
      }
      else
      {
        return 56 + file;
      }
    }


    /// <summary>
    /// Worker method to do FEN parsing.
    /// NOTE: performance could be improved by passing in the Piece[] preallocated
    /// </summary>
    /// <param name="fen"></param>
    /// <param name="repetitionCount"></param>
    /// <returns></returns>
    static FENParseResult DoParseFEN(string fen, int repetitionCount = 0)
    {
      int charIndex = 0;

      void SkipAnySpaces() { while (charIndex < fen.Length && char.IsWhiteSpace(fen[charIndex])) charIndex++; }
      void SkipAnySpacesOrDash() { while (charIndex < fen.Length && (fen[charIndex] == '-' || char.IsWhiteSpace(fen[charIndex]))) charIndex++; }

      List<PieceOnSquare> pieces = new List<PieceOnSquare>(32);

      int curRank = 0;
      int curFile = 0;
      //(int,int) wKing;
      //(int,int) bKing;
      //(int, int) wRooks;
      //(int, int) bRooks;

      // Parse pieces
      while (true)
      {
        char thisChar = fen[charIndex++];

        if (thisChar == ' ') break;

        if (thisChar == '/')
        {
          if (curRank >= 7) throw new Exception("Illegal FEN, too many squares");
          curRank++;
          curFile = 0;
        }
        else if (char.IsDigit(thisChar))
        {
          curFile += (thisChar - '0');
          if (curFile > 8) throw new Exception("Illegal FEN, too many files");
        }
        else
        {
          Piece thisPiece = byteToPieces[thisChar];
          //if (thisPiece.Type == PieceType.King && char.IsUpper(thisChar))
          //  wKing = (curFile,curRank);
          //else if (thisPiece.Type == PieceType.King && char.IsLower(thisChar))
          //  bKing = (curFile, curRank);
          //else if (thisPiece.Type == PieceType.Rook && char.IsUpper(thisChar))
          //  wRooks = (curFile, curRank);
          //else if (thisPiece.Type == PieceType.Rook && char.IsLower(thisChar))
          //  bRooks = (curFile, curRank);
          if (thisPiece.Type == PieceType.None) throw new Exception("Illegal FEN, found piece character " + thisChar);
          pieces.Add(new PieceOnSquare(Square.FromFileAndRank(curFile, 7 - curRank), thisPiece));
          curFile++;
        }
      }

      SkipAnySpaces();

      // 2. Side to move character
      char sideMoveChar = char.ToLower(fen[charIndex++]);
      SideType sideToMove;
      if (sideMoveChar == 'b')
        sideToMove = SideType.Black;
      else if (sideMoveChar == 'w')
        sideToMove = SideType.White;
      else
        throw new Exception($"Illegal FEN, side to move character {sideMoveChar}");

      SkipAnySpaces();

      // 3. Castling availability

        // 3. Castling availability
      bool whiteCanOO = false;
      bool whiteCanOOO = false;
      bool blackCanOO = false;
      bool blackCanOOO = false;

      bool isChess960 = false;

      // Extract the castling rights portion from the FEN string
      int nextSpaceIndex = fen.IndexOf(' ', charIndex);
      string castlingRights;

      if (nextSpaceIndex != -1)
      {
        // There is a space, so extract the substring up to that space
        castlingRights = fen.Substring(charIndex, nextSpaceIndex - charIndex);
      }
      else
      {
        // No space found, assume castling rights go to the end of the FEN string
        castlingRights = fen.Substring(charIndex);
      }

      // Only proceed if there are castling rights specified (i.e., the string is not "-")
      if (castlingRights != "-")
      {
        // Check if castling rights contain any non-standard chess characters
        isChess960 = castlingRights.IndexOfAny(new char[] { 'K', 'Q', 'k', 'q' }) == -1;
                
        MGPosition pos = default;
        Square whiteKingSquare = default;
        Square blackKingSquare = default;
        List<Square> whiteRookSquares = new List<Square>();
        List<Square> blackRookSquares = new List<Square>();

        foreach (PieceOnSquare ps in pieces)
        {
          var mgPiece = MGChessPositionConverter.MGPieceFromPiece(ps.Piece);
          var mgSquare = MGPosition.MGBitBoardFromSquare(ps.Square);
          pos.SetPieceAtBitboardSquare((ulong)mgPiece, mgSquare);

          // Identify king and rook positions
          if (ps.Piece.Type == PieceType.King)
          {
            if (ps.Piece.Side == SideType.White)
              whiteKingSquare = ps.Square;
            else
              blackKingSquare = ps.Square;
          }
          else if (ps.Piece.Type == PieceType.Rook)
          {
            if (ps.Piece.Side == SideType.White)
              whiteRookSquares.Add(ps.Square);
            else
              blackRookSquares.Add(ps.Square);
          }
        }
        
        ulong wKing = (~pos.D & pos.C & pos.B & pos.A) & QBBoperations.firstRank;        
        int whiteKingSq = (int)QBBoperations.LSB(wKing);
        ulong bKing = (pos.D & pos.C & pos.B & pos.A) & QBBoperations.lastRank;
        int blackKingSq = (int)QBBoperations.LSB(bKing);
        ulong wRooks = ~pos.D & pos.C & ~pos.B & ~pos.A; //& QBBoperations.firstRank;
        ulong bRooks = (pos.D & pos.C & ~pos.B & ~pos.A); //& QBBoperations.lastRank;
        //var kPiece = MGChessPositionConverter.PieceAt(in pos, kingSq);
        int wRookLongSq = (int)(QBBoperations.MSB(wRooks & QBBoperations.firstRank));
        int wRookShortSq = (int)(QBBoperations.LSB(wRooks & QBBoperations.firstRank));
        //var kPiece = MGChessPositionConverter.PieceAt(in pos, kingSq);
        int bRookLongSq = (int)(QBBoperations.MSB(bRooks & QBBoperations.lastRank));
        int bRookShortSq = (int)(QBBoperations.LSB(bRooks & QBBoperations.lastRank));
        
        // Variables to hold the rook positions
        char whiteKingSideRook = ' ';
        char whiteQueenSideRook = ' ';
        char blackKingSideRook = ' ';
        char blackQueenSideRook = ' ';

        // If it's Chess960, assign the rooks based on castling rights
        if (isChess960)
        {          
          foreach (char c in castlingRights)
          {
            var color = Char.IsUpper(c);
            var curRookPlacement = CharToNumber(c, color);
            if (char.IsUpper(c)) // White's castling rights
            {
              var shortCastling = wRookShortSq < whiteKingSq && wRookShortSq == curRookPlacement;
              var longCastling = wRookLongSq > whiteKingSq && wRookLongSq == curRookPlacement;
              if (shortCastling) // Assign the first character as king-side rook
                whiteKingSideRook = c;
              else if (longCastling)
                whiteQueenSideRook = c; // The second character as queen-side rook
            }
            else if (char.IsLower(c)) // Black's castling rights
            {
              var shortCastling = bRookShortSq < blackKingSq && bRookShortSq == curRookPlacement;
              var longCastling = bRookLongSq > blackKingSq && bRookLongSq == curRookPlacement;
              if (shortCastling) // Assign the first character as king-side rook
                blackKingSideRook = c;
              else if (longCastling)
                blackQueenSideRook = c; // The second character as queen-side rook
            }
          }
        }
        else
        {
          // Standard chess rook positions (for completeness)
          whiteKingSideRook = 'H';
          whiteQueenSideRook = 'A';
          blackKingSideRook = 'h';
          blackQueenSideRook = 'a';
        }

        // Process the castling rights in the FEN string
        foreach (char thisChar in castlingRights)
        {
          if (thisChar == 'K' || thisChar == whiteKingSideRook)
            whiteCanOO = true;
          else if (thisChar == 'Q' || thisChar == whiteQueenSideRook)
            whiteCanOOO = true;
          else if (thisChar == 'k' || thisChar == blackKingSideRook)
            blackCanOO = true;
          else if (thisChar == 'q' || thisChar == blackQueenSideRook)
            blackCanOOO = true;
        }

        charIndex += castlingRights.Length;
      }




      // 3. Castling availability

      //bool whiteCanOO = false;
      //bool whiteCanOOO = false;
      //bool blackCanOO = false;
      //bool blackCanOOO = false;
      //while (charIndex < fen.Length)
      //{
      //  char thisChar = fen[charIndex++];
      //  if (thisChar == 'K' || thisChar == 'H')
      //    whiteCanOO = true;
      //  else if (thisChar == 'Q' || thisChar == 'A')
      //    whiteCanOOO = true;
      //  else if (thisChar == 'k' || thisChar == 'h')
      //    blackCanOO = true;
      //  else if (thisChar == 'q' || thisChar == 'a')
      //    blackCanOOO = true;
      //  else if (thisChar == ' ')
      //    break;
      //}

      SkipAnySpaces();

      // 4. En passant target square
      int numEPChars = 0;
      PositionMiscInfo.EnPassantFileIndexEnum epColIndex = PositionMiscInfo.EnPassantFileIndexEnum.FileNone;
      int epFile = 0;
      int epRank = 0;
      while (charIndex < fen.Length)
      {
        // Try to work around error in FEN whereby dash is missing, and move number immediately follows
        if (numEPChars == 0 && char.IsDigit(fen[charIndex]))
          break;

        char thisChar = fen[charIndex++];
        if (thisChar == '-' || thisChar == ' ')
          break;
        else
        {
          if (numEPChars == 0)
            epFile = char.ToLower(thisChar) - 'a';
          else if (numEPChars == 1)
            epRank = char.ToLower(thisChar) - '0';
          else
            throw new Exception("too many en passant characters");

          numEPChars++;
        }
      }

      SkipAnySpaces();

      if (numEPChars > 0)
      {
        if (numEPChars == 2)
        {
          if (epFile > (byte)PositionMiscInfo.EnPassantFileIndexEnum.FileH)
            throw new Exception("Invalid en passant file in FEN");
          else
            epColIndex = (PositionMiscInfo.EnPassantFileIndexEnum)epFile;
        }
        else
          throw new Exception("Invalid en passant in FEN");
      }

      SkipAnySpacesOrDash(); // Sometimes we see ill-formed FENs that end with extaneous dash, e.g. "w q - -" 

      // 5. Halfmove clock: This is the number of halfmoves since the last capture or pawn advance. 
      int move50Count = 0;
      while (charIndex < fen.Length && charIndex < fen.Length)
      {
        char thisChar = fen[charIndex++];
        if (thisChar == ' ')
          break;
        else
          move50Count = move50Count * 10 + (thisChar - '0');
      }

      SkipAnySpaces();

      // Fullmove number
      int fullmoveCount = 0;
      while (charIndex < fen.Length && charIndex < fen.Length)
      {
        char thisChar = fen[charIndex++];
        if (!char.IsDigit(thisChar))
        {
          break;
        }
        else
        {
          fullmoveCount = fullmoveCount * 10 + (thisChar - '0');
        }
      }

      int plyCount;
      if (fullmoveCount == 0)
      {
        // Fill in move count with 1 if none found
        plyCount = 1;
      }
      else
      {
        // Convert from moves to ply
        plyCount = fullmoveCount * 2;
      }

      if (sideToMove == SideType.Black)
      {
        plyCount++;
      }

      if (charIndex < fen.Length - 1)
      {
        string remainder = fen.Substring(charIndex);
        if (StringUtils.WhitespaceRemoved(remainder) != "")
        {
          throw new Exception($"Unexpected characters after FEN {remainder} ");
        }
      }

      PositionMiscInfo miscInfo = new PositionMiscInfo(whiteCanOO, whiteCanOOO, blackCanOO, blackCanOOO,
                                                        sideToMove, move50Count, repetitionCount, plyCount, epColIndex);

      return new FENParseResult(pieces, miscInfo);
    }

    #region Internal statics

    static Piece[] byteToPieces;

    /// <summary>
    /// Static initializer to initialize bytesToPieces array.
    /// </summary>
    static FENParser()
    {
      byteToPieces = new Piece[byte.MaxValue - 1];

      byteToPieces['K'] = (White, King);
      byteToPieces['Q'] = (White, Queen);
      byteToPieces['R'] = (White, Rook);
      byteToPieces['B'] = (White, Bishop);
      byteToPieces['N'] = (White, Knight);
      byteToPieces['P'] = (White, Pawn);

      byteToPieces['k'] = (Black, King);
      byteToPieces['q'] = (Black, Queen);
      byteToPieces['r'] = (Black, Rook);
      byteToPieces['b'] = (Black, Bishop);
      byteToPieces['n'] = (Black, Knight);
      byteToPieces['p'] = (Black, Pawn);
    }

    #endregion


  }
}
