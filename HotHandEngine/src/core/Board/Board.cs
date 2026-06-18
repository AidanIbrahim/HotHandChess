using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


//Credit to Sebastian Lague for inspiration and some of the code structure. His Repository: https://github.com/SebLague/Chess-Coding-Adventure/

namespace Chess.Core
{
	public class Board
	{
		//Chess Constants
		public const int BOARD_SIZE = 64; //Number of squares on the chess board.
		public const int NUM_COLS = 8;
		public const int NUM_ROWS = 8;

		// Indexes for the colors. Used to access the piece bitboards and other color specific data.
		public const int whiteIndex = 0;
		public const int blackIndex = 1;
		

		//Array that stores piece codes, indexed by square. Little Endian, index 0 is a1, index 63 is h8.
		public int[] chessboard = new int[BOARD_SIZE];


        //BitBoards for each piece of each color. Indexing is as follows:
        //0-5: White Pawn, Knight, Bishop, Rook, Queen, King
        //6-11: Black Pawn, Knight, Bishop, Rook, Queen, King
        //Formula for accessing: (colorIndex * 6) + pieceTypeIndex
        public ulong[] pieceBitBoards = new ulong[12];

		//BitBoards for all pieces of each color. 
		public ulong[] colorBitboards = new ulong[2];
		public ulong allPiecesBitboard;

		//Constants
		const string fenDefaultCommand = "position rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        //Function definitions start here.

        //Creates a new board
        public Board()
		{
			for (int board = 0; board < colorBitboards.Length; board++)
			{
				colorBitboards[board] = 0; //Initialize color bitboards
			}
			for (int board = 0; board < pieceBitBoards.Length; board++)
			{
				pieceBitBoards[board] = 0; //Initialize piece bitboards
			}
			allPiecesBitboard = 0;
		}

		//Read FEN notation, then update chessboard accoirdingly, this will then be used to load all the other bitboards
		//rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
		public void parseFenToChessboard(string message)
		{
			string[] tokens = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			string fenString = tokens[1];

			if (fenString == "startpos")
			{
                tokens = fenDefaultCommand.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                fenString = tokens[1];
			}

			string[] fenTokens = fenString.Split('/'); //Each entry should be read left to right, token 1 is rank 8 and token 8 is rank 1
			int fenIndex = 0;

			for (int rank = 0; rank < NUM_ROWS; rank++)
			{
				string fenRank = fenTokens[rank];
				fenIndex = 0;

				int boardIndex = (NUM_ROWS - 1 - rank) * 8;

				while (fenIndex < fenRank.Length)
				{
					char fenChar = fenRank[fenIndex];

					if (char.IsDigit(fenChar))
					{
						int numEmptySquares = fenChar - '0';
						for (int e = 0; e < numEmptySquares; e++)
						{
							chessboard[boardIndex++] = Piece.NONE;
                        }
						fenIndex++;
	
					} else
					{
						chessboard[boardIndex++] = Piece.symbolToPieceCode(fenChar);
						fenIndex++;
					}
				}
			
			}
			updateBitboardsFromChessboard(); //Update the corresponding piece bitboard
		}
        //Updates the chessboard representation, and then updates the bitboards

		//Sets bit square in bitboard to 1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong setBit(ulong BitBoard, int square)
		{
			return BitBoard | (1UL << square);
		}

		//Sets bit square in bitboard to 0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong clearBit(ulong BitBoard, int square)
        {
            return BitBoard & ~(1UL << square);
        }


        //Reads in the current board state to all bitboards
        public void updateBitboardsFromChessboard()
		{
            Array.Clear(pieceBitBoards, 0, pieceBitBoards.Length);

            for (int i = 0; i < BOARD_SIZE; i++)
			{
				if (chessboard[i] != -1)
				{
					pieceBitBoards[chessboard[i]] = setBit(pieceBitBoards[chessboard[i]], i);
				}
			}

			colorBitboards[Piece.WHITE] = 0;
			colorBitboards[Piece.BLACK] = 0;
            for (int i = Piece.PIECE_INDEX_MIN; i < Piece.PIECE_INDEX_MAX; i++)
            {
				if (i < Piece.BLACK_PAWN)
				{
					colorBitboards[Piece.WHITE] |= pieceBitBoards[i];
				} else
				{
                    colorBitboards[Piece.BLACK] |= pieceBitBoards[i];
                }
            }

			allPiecesBitboard = colorBitboards[Piece.BLACK] | colorBitboards[Piece.WHITE];
        }


		//print a square based representation to console, used for debugging
        public void printDebug()
		{
			for (int rank = 7; rank >= 0; rank--)
			{
				for (int file = 0; file < 8; file++)
				{
					int index = rank * 8 + file;
					Console.Write($"{Piece.getPieceSymbol(chessboard[index])} ");
				}
				Console.WriteLine();
			}
		}

		//Print a BitBoard to console, used for debugging
        public void printBitboards(ulong bitboard)
        {
            for (int rank = 7; rank >= 0; rank--)
            {
                for (int file = 0; file < 8; file++)
                {
                    int square = rank * 8 + file;
                    ulong mask = 1UL << square;

                    Console.Write((bitboard & mask) != 0 ? "1 " : ". ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }




    }
}