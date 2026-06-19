using System;
using System.Runtime.CompilerServices;
using System.Xml;
using Chess.Core;

namespace HotHandEngine;

public struct ParsedMove
{
	int fromSquare;
	int toSquare;
	int promoPiece;

	public ParsedMove(int from, int to, int pieceType)
	{
		fromSquare = from;
		toSquare = to;
		promoPiece = pieceType; //Should be Piece.NONE if not a promotion
	}
}

public class Engine
{
	Board engineBoard;

    public Engine()
	{
       engineBoard = new Board(); //Creates a new board 
    }

	//Clears out the board to start a new game
	public void newGame()
	{
		engineBoard = new Board();
	}

	//Loads a position from from a fen string, passes command details to the Board class
	public void loadPosition(string command)
	{
        engineBoard.parseFenToChessboard(command);
		return;
	}

    //Takes chess algebraic notation i.e e4e5 and returns a move struct, 

    public ParsedMove parseMoveText(string ChessNotation)
	{
        //Example: a7a8r a7 moves to a8, and promotes to a rook
        //The color of the piece need not be specified, just the square the piece moves from, the square it moves to, and the type of piece it promotes to if applicable

        //Break up the message into it's parts ["a7"]["a8"]["r"] (Promotion Case) or ["e4"]["e5"] (No Promotion Case)

        //UCI moves are always 4 or 5 characters:
        //  - First 2 chars: FROM square (e.g., "a7")
        //  - Next 2 chars:  TO square   (e.g., "a8")
        //  - 5th char:      Promotion piece (if present)

        //Hint: use the substring function "ChessNotation.Substring(startIndex, length)" https://learn.microsoft.com/en-us/dotnet/api/system.string.substring?view=net-10.0

        //Convert the first token into the integer square index, and assign it to moveFrom. "a7" = 48

        //Remember, each square on the board has a corresponding integer value 0-63, a1 is 0, h8 is 63
        //It may be beneficial to create a helper function for this that takes the text and returns a number

        int moveFrom = 0; //IMPLEMENT

		//Do the same for the second token, and assign it to moveTo

		int moveTo = 0; //IMPLEMENT

		//Finally, get the piece type for promotion. If there is no promotion, it should be Piece.NONE.
		//Otherwise it should be Piece.QUEEN, Piece.ROOK, ect.
		//p n b r q k : pawn knight bishop rook queek king

		int promotion = Piece.NONE;

		//Don't forget to clean up these comments and put your own before finishing.

		//Return
		return new ParsedMove(moveFrom, moveTo, promotion);
    }


	//Searches for the best move and returns it
	public string getMove(string command)
	{
		return "d7d5"; //DEBUG MOVE FOR TESTING, THIS WILL RETURN RESULTS FROM SEARCH
	}

	//Halts the search and returns the best move currently found
	public string cancelSearch()
	{
        return "d7d5"; //DEBUG MOVE FOR TESTING, THIS WILL RETURN RESULTS FROM SEARCH
    }

	//Debug command to print the square position
	public void printPosition()
	{
		Console.WriteLine("CURRENT BOARD STATE");
		engineBoard.printDebug();
        Console.WriteLine("------------------------");

    }

    public void printEngineBitBoards()
    {
        for (int i = Piece.PIECE_INDEX_MIN; i <= Piece.PIECE_INDEX_MAX; i++)
		{
            Console.WriteLine($"BITBOARD: {Piece.getPieceSymbol(i)}");
			engineBoard.printBitboards(engineBoard.pieceBitBoards[i]);
            Console.WriteLine("------------------------");
        }

    }
}
