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


		//BitBoards for each piece of each color. Indexing is as follows:
		//0-5: White Pawn, Knight, Bishop, Rook, Queen, King
		//6-11: Black Pawn, Knight, Bishop, Rook, Queen, King
		//Formula for accessing: (colorIndex * 6) + pieceTypeIndex
		public ulong[] pieceBitBoards;

		//BitBoards for all pieces of each color. 
		public ulong[] colorBitBoards;





	}























}