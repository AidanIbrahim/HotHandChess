using System;
using System.Collections.Generic;

//Credit to Sebastian Lague for inspiration and some of the code structure. His Repository: https://github.com/SebLague/Chess-Coding-Adventure/

namespace Chess.Core
{
	public class Board
	{
		// Indexes for the colors. Used to access the piece bitboards and other color specific data.
		public const int whiteIndex = 0;
		public const int blackIndex = 1;

		//Array that stores piece codes, indexed by square. Little Endian, index 0 is a1, index 63 is h8.
		public int[] chessboard;


        //BitBoards for each piece of each color. Indexing is as follows:
        //0-5: White Pawn, Knight, Bishop, Rook, Queen, King
        //6-11: Black Pawn, Knight, Bishop, Rook, Queen, King
        //Formula for accessing: (colorIndex * 6) + pieceTypeIndex
        public ulong[] pieceBitBoards = new ulong[12];

		//BitBoards for all pieces of each color. 
		public ulong[] colorBitboards = new ulong[2];
		public ulong allPiecesBitboard;

		public int isWhiteTurn = 1;


		//Function definitions start here.

		//Creates a new board
		public Board()
		{
			chessboard = new int[64];
			//Load up all the values to their defaults
		}

		//Updates the chessboard representation, and then updates the bitboards
		public void parseFenToChessboard()
		{

			return;
		}

		public void updateBitboardsFromChessboard()
		{

			return;
		}
		

    }























}