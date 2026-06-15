using System;
using System.Collections.Generic;
using System.ComponentModel;

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
		}
        //Updates the chessboard representation, and then updates the bitboards

        public void updateBitboardsFromChessboard()
		{

			return;
		}

		public void printDebug()
		{
			for (int rank = 7; rank >= 0; rank--)
			{
				for (int file = 0; file < 8; file++)
				{
					int index = rank * 8 + file;
					Console.Write(Piece.getPieceSymbol(chessboard[index]));
				}
				Console.WriteLine();
			}
		}

		

    }
}