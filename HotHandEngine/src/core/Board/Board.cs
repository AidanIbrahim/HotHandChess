using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;


//Credit to Sebastian Lague for inspiration and some of the code structure. His Repository: https://github.com/SebLague/Chess-Coding-Adventure/

namespace Chess.Core
{

	public class GameState
	{
		public int colorToMove = Piece.WHITE;
		public int enPassantSquare = -1; //-1 is NULL
		uint castleRights = 0b1111; //W Kingside, W Queenside, B Kingside Side, B Queenside

        //Hardcoded Castling Moves, pregenerate these in this class, then return them when queried for legal castles
        public Move whiteKingsideMove = new Move(Square.E1, Square.G1, Piece.WHITE_KING, Piece.NONE, Piece.NONE, false, true, false, 0);
        public Move whiteQueensideMove = new Move(Square.E1, Square.C1, Piece.WHITE_KING, Piece.NONE, Piece.NONE, false, true, false, 0);
        public Move blackKingsideMove = new Move(Square.E8,Square.G8, Piece.BLACK_KING, Piece.NONE, Piece.NONE, false, true, false, 0);
        public Move blackQueensideMove = new Move(Square.E8, Square.C8, Piece.BLACK_KING, Piece.NONE, Piece.NONE, false, true, false, 0);

		public GameState(GameState other)
		{
			this.colorToMove = other.colorToMove;
			this.enPassantSquare = other.enPassantSquare;
			this.castleRights = other.castleRights;
		}

        public void resetCastleRights() //Sets all bits of castleRights to 1
		{
			castleRights = 0b1111;
        }

        public bool wKCastle => ((castleRights >> 0) & 1) != 0;
        public bool wQCastle => ((castleRights >> 1) & 1) != 0;
        public bool bKCastle => ((castleRights >> 2) & 1) != 0;
        public bool bQCastle => ((castleRights >> 3) & 1) != 0;

        public void setBitCastle(int bit)
        {
			castleRights |= (1U << bit);
            return;
        }
        public void clearBitCastle(int bit)
        {
            castleRights &= ~(1U << bit);
        }


        public Move[] getCastleRights(int colorToGo) //Returns castling rights given a side. 
		{

            if (castleRights == 0) //Skip operation if no castles are allowed
                return Array.Empty<Move>();

            List<Move> legalCastles = new List<Move>();

            if (colorToGo == Piece.WHITE) //White to go case
			{
				if (wKCastle && checkCastleLegality(whiteKingsideMove))
				{
                    legalCastles.Add(whiteKingsideMove);
                }

                if (wQCastle && checkCastleLegality(whiteQueensideMove))
                {
                    legalCastles.Add(whiteQueensideMove);
                }

            } else //Black to go case
			{
                if (bKCastle && checkCastleLegality(blackKingsideMove))
                {
                    legalCastles.Add(blackKingsideMove);
                }

                if (bQCastle && checkCastleLegality(blackQueensideMove))
                {
                    legalCastles.Add(blackQueensideMove);
                }
            }

			return legalCastles.ToArray();
		}

		public bool checkCastleLegality(Move castleMove)
		{
			//Legality Logic Here, implement later
			//A king cannot castle when in check, if either the rook or king has moved, the king is in check, or if castling takes the king through check.

			return true;
		}

		public void flipToGo() //Switches colorToGo to the other color
		{
			if (colorToMove == Piece.WHITE)
			{
				colorToMove = Piece.BLACK;
			} else
			{
				colorToMove = Piece.WHITE;
			}
		}

	}

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


        //Bitboards for each piece of each color. Indexing is as follows:
        //1-6: White Pawn, Knight, Bishop, Rook, Queen, King
        //7-12: Black Pawn, Knight, Bishop, Rook, Queen, King
        public ulong[] pieceBitboards = new ulong[12];

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
			for (int board = 0; board < pieceBitboards.Length; board++)
			{
				pieceBitboards[board] = 0; //Initialize piece bitboards
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
			reloadBitboardsFromChessboard(); //Update the corresponding piece bitboard
		}
        //Updates the chessboard representation, and then updates the bitboards

		//Sets bit square in bitboard to 1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong setBit(ulong Bitboard, int square)
		{
			return Bitboard | (1UL << square);
		}

		//Sets bit square in bitboard to 0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong clearBit(ulong Bitboard, int square)
        {
            return Bitboard & ~(1UL << square);
        }

		//Make Move Functions
		public void makeMove(Move ChessMove)
		{
			//Branches based on move type
			if (ChessMove.castle) { makeCastleMove(ChessMove); return; }
            if (ChessMove.epFlag) { makeEnPassantMove(ChessMove); return; }
            if (ChessMove.dblPawn) { makeDoublePawnMove(ChessMove); return; }

			makeNormalMove(ChessMove);
        }

		public void makeNormalMove(Move ChessMove)
		{
            chessboard[ChessMove.toSquare] = ChessMove.movePiece; //Update Square based representation
            chessboard[ChessMove.fromSquare] = Piece.NONE;

            //Bitmasks for move from and both squares
            ulong fromMask = 1UL << ChessMove.fromSquare;
            ulong toMask = 1UL << ChessMove.toSquare;
            ulong moveMask = fromMask | toMask;

            //XOR flips both the to and from bits
            pieceBitboards[ChessMove.movePiece] ^= moveMask;

            //Update color bitboard for Mover
            if (Piece.isWhite(ChessMove.movePiece))
                colorBitboards[Piece.WHITE] ^= moveMask;
            else
                colorBitboards[Piece.BLACK] ^= moveMask;

            //Handle Capture
            if (ChessMove.capturedPiece != Piece.NONE)
            {
                pieceBitboards[ChessMove.capturedPiece] &= ~toMask; //Update Piece Board

                if (Piece.isWhite(ChessMove.capturedPiece))
                {
                    colorBitboards[Piece.WHITE] &= ~toMask;
                }
                else
                {
                    colorBitboards[Piece.BLACK] &= ~toMask;
                }
            }

            allPiecesBitboard = colorBitboards[Piece.WHITE] | colorBitboards[Piece.BLACK];

        }

		public void makeDoublePawnMove(Move ChessMove)
		{

		}

		public void makePromoMove(Move ChessMove)
		{

		}

		public void makeEnPassantMove(Move ChessMove)
		{

		}

		public void makeCastleMove(Move ChessMove)
		{

		}

        //Unmake Move Functions

        public void unmakeNormalMove(Move ChessMove)
		{
            if (ChessMove.castle || ChessMove.dblPawn || ChessMove.epFlag || ChessMove.promoPiece != Piece.NONE)
            {
                //Special Move Cases
            }
            else
            {
                chessboard[ChessMove.fromSquare] = ChessMove.movePiece;
                chessboard[ChessMove.toSquare] = ChessMove.capturedPiece;
            }
        }

        //Reads in the current board state to all bitboards
        public void reloadBitboardsFromChessboard()
		{
            Array.Clear(pieceBitboards, 0, pieceBitboards.Length);

            for (int i = 0; i < BOARD_SIZE; i++)
			{
				if (chessboard[i] != -1)
				{
					pieceBitboards[chessboard[i]] = setBit(pieceBitboards[chessboard[i]], i);
				}
			}

			colorBitboards[Piece.WHITE] = 0;
			colorBitboards[Piece.BLACK] = 0;
            for (int i = Piece.PIECE_INDEX_MIN; i < Piece.PIECE_INDEX_MAX; i++)
            {
				if (i < Piece.BLACK_PAWN)
				{
					colorBitboards[Piece.WHITE] |= pieceBitboards[i];
				} else
				{
                    colorBitboards[Piece.BLACK] |= pieceBitboards[i];
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