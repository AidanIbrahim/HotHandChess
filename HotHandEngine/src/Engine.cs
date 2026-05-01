using System;
using Chess.Core;


namespace HotHandEngine;

public class Engine
{
	Board engineBoard = new Board();

    public Engine()
	{

	}


	public void loadPosition(string command)
	{

        engineBoard.parseFenToChessboard(command);
		return;
	}

	//Debug command to print the square position
	public void printPosition()
	{
		Console.WriteLine("CURRENT BOARD STATE");
		engineBoard.printDebug();
        Console.WriteLine("------------------------");

    }
}
