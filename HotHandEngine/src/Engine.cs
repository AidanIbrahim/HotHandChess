using System;
using System.Runtime.CompilerServices;
using Chess.Core;


namespace HotHandEngine;

public class Engine
{
	Board engineBoard = new Board();

    public Engine()
	{

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

	public string makeMove()
	{
		return "IMPLEMENT MAKE MOVE";
	}

	//Debug command to print the square position
	public void printPosition()
	{
		Console.WriteLine("CURRENT BOARD STATE");
		engineBoard.printDebug();
        Console.WriteLine("------------------------");

    }
}
