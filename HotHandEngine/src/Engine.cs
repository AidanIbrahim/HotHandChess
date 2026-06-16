using System;
using System.Runtime.CompilerServices;
using Chess.Core;


namespace HotHandEngine;

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
