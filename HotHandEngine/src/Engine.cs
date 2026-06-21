using System;
using System.Runtime.CompilerServices;
using System.Xml;
using Chess.Core;

namespace HotHandEngine;

public class ParsedMove
{
    public int moveFrom;
    public int moveTo;
    public int promotion;

    public ParsedMove(int moveFrom, int moveTo, int promotion)
    {
        this.moveFrom = moveFrom;
        this.moveTo = moveTo;
        this.promotion = promotion;
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

    public ParsedMove parseMoveText(String ChessNotation)
    {
        ChessNotation = ChessNotation.Trim().ToLower();

        string fromSquare = ChessNotation.Substring(0, 2);
        string toSquare = ChessNotation.Substring(2, 2);

        int moveFrom = squareTextToIndex(fromSquare);
        int moveTo = squareTextToIndex(toSquare);

        int promotion = Piece.NONE;

        if (ChessNotation.Length == 5)
        {
            char promotionPiece = ChessNotation[4];

            switch (promotionPiece)
            {
                case 'q':
                    promotion = Piece.QUEEN;
                    break;
                case 'r':
                    promotion = Piece.ROOK;
                    break;
                case 'b':
                    promotion = Piece.BISHOP;
                    break;
                case 'n':
                    promotion = Piece.KNIGHT;
                    break;
                default:
                    promotion = Piece.NONE;
                    break;
            }
        }

        return new ParsedMove(moveFrom, moveTo, promotion);
    }

    private int squareTextToIndex(string squareText)
    {
        int fileChar = squareText[0] - 'a';
        int rankChar = squareText[1] - '1';
        return rankChar * 8 + fileChar;
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
            engineBoard.printBitboards(engineBoard.pieceBitboards[i]);
            Console.WriteLine("------------------------");
        }

    }
}
