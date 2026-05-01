using System;
using System.Collections.Generic;

//Credit to Sebastian Lague for inspiration and some of the code structure. His Repository: https://github.com/SebLague/Chess-Coding-Adventure/

namespace Chess.Core
{

    //This Class is used to define the piece codes and other constants related to the chess pieces.

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

        public const int NUM_PIECE_TYPES= 6; // Number of pieces per color

        public const int WHITE_PAWN = PAWN + WHITE * NUM_PIECE_TYPES;        // 0
        public const int WHITE_KNIGHT = KNIGHT + WHITE * NUM_PIECE_TYPES;    // 1
        public const int WHITE_BISHOP = BISHOP + WHITE * NUM_PIECE_TYPES;    // 2
        public const int WHITE_ROOK = ROOK + WHITE * NUM_PIECE_TYPES;        // 3
        public const int WHITE_QUEEN = QUEEN + WHITE * NUM_PIECE_TYPES;      // 4
        public const int WHITE_KING = KING + WHITE * NUM_PIECE_TYPES;        // 5

        public const int BLACK_PAWN = PAWN + BLACK * NUM_PIECE_TYPES;        // 6
        public const int BLACK_KNIGHT = KNIGHT + BLACK * NUM_PIECE_TYPES;    // 7
        public const int BLACK_BISHOP = BISHOP + BLACK * NUM_PIECE_TYPES;    // 8
        public const int BLACK_ROOK = ROOK + BLACK * NUM_PIECE_TYPES;        // 9
        public const int BLACK_QUEEN = QUEEN + BLACK * NUM_PIECE_TYPES;      // 10
        public const int BLACK_KING = KING + BLACK * NUM_PIECE_TYPES;        // 11

        public const int PIECE_INDEX_MAX = BLACK_KING; // Used for iterating, should be set to the highest piece code.
        public const int PIECE_INDEX_MIN = WHITE_PAWN; // Used for iterating, should be set to the lowest piece code.

        //Function definitions start here.

        //Converts alphabetical characters to integer piece codes. Lowercase are black pieces, and uppercase are white pieces
        public static int symbolToPieceCode(char pieceSymbol)
        {
            int pieceCode = pieceSymbol switch
            {
                'p' => BLACK_PAWN,
                'r' => BLACK_ROOK,
                'n' => BLACK_KNIGHT,
                'b' => BLACK_BISHOP,
                'q' => BLACK_QUEEN,
                'k' => BLACK_KING,

                'P' => WHITE_PAWN,
                'R' => WHITE_ROOK,
                'N' => WHITE_KNIGHT,
                'B' => WHITE_BISHOP,
                'Q' => WHITE_QUEEN,
                'K' => WHITE_KING,

                _ => NONE

            };
            return pieceCode;
        }

        public static char getPieceSymbol(int pieceCode)
        {
            char symbol = (pieceCode % NUM_PIECE_TYPES) switch
            {
                ROOK => 'R',
                KNIGHT => 'N',
                BISHOP => 'B',
                QUEEN => 'Q',
                KING => 'K',
                PAWN => 'P',
                _ => ' '
            };

            //NONE is -1, and should match to the catch all case

            return isWhite(pieceCode) ?  symbol : char.ToLower(symbol);

        }

        //FIX OUTPUT OF THIS FUNCTION, NO INTO TO BOOL CASTING

        //Returns a bool, true if a piece is white
        //Does not input validate for NONE case. Should not be called on a pieceType with value NONE
        public static bool isWhite(int pieceType)
        {
            int color = pieceType / NUM_PIECE_TYPES; //Int division will return 0 if the id is lower than 5, but 1 if it is higher than 6
            if (color == WHITE) return true;
            else return false;
        }
    }























}