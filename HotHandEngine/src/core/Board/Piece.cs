using System;
using System.Collections.Generic;

//Credit to Sebastian Lague for inspiration and some of the code structure. His Repository: https://github.com/SebLague/Chess-Coding-Adventure/

namespace Chess.Core
{
    public static class Piece
    {
        //Codes for the pieces. Used for indexing and other purposes.
        public const int PAWN = 0;
        public const int KNIGHT = 1;
        public const int BISHOP = 2;
        public const int ROOK = 3;
        public const int QUEEN = 4;
        public const int KING = 5;
        public const int NONE = -1;

        //Piece colors
        public const int WHITE = 0;
        public const int BLACK = 1;

        public const int PIECE_PER_COLOR = 6; // Number of pieces per color

        public const int WHITE_PAWN = PAWN + WHITE * PIECE_PER_COLOR;        // 0
        public const int WHITE_KNIGHT = KNIGHT + WHITE * PIECE_PER_COLOR;    // 1
        public const int WHITE_BISHOP = BISHOP + WHITE * PIECE_PER_COLOR;    // 2
        public const int WHITE_ROOK = ROOK + WHITE * PIECE_PER_COLOR;        // 3
        public const int WHITE_QUEEN = QUEEN + WHITE * PIECE_PER_COLOR;      // 4
        public const int WHITE_KING = KING + WHITE * PIECE_PER_COLOR;        // 5

        public const int BLACK_PAWN = PAWN + BLACK * PIECE_PER_COLOR;        // 6
        public const int BLACK_KNIGHT = KNIGHT + BLACK * PIECE_PER_COLOR;    // 7
        public const int BLACK_BISHOP = BISHOP + BLACK * PIECE_PER_COLOR;    // 8
        public const int BLACK_ROOK = ROOK + BLACK * PIECE_PER_COLOR;        // 9
        public const int BLACK_QUEEN = QUEEN + BLACK * PIECE_PER_COLOR;      // 10
        public const int BLACK_KING = KING + BLACK * PIECE_PER_COLOR;        // 11

        public const int PIECE_INDEX_MAX = BLACK_KING; // Used for iterating, should be set to the highest piece code.

    }























}