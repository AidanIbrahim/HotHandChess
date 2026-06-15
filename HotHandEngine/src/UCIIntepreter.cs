using Chess.Core;
using System;
using System.IO;

namespace HotHandEngine;

public class UCIIntepreter
{
    //Intepreter Settings. Rough testing implementation, these should be pulled from a settings file or the bot classes later.
    bool logInteraction = true; //If engine will log the interactions to a text file
    string logPath = "logs"; //Path to the log file
    string logFileName = "UCI_Log.txt";
    string engineName = "HotHandEngine"; //The name of the engine that is being run
    Engine chessEngine = new Engine();
    

    //Function definitions start here

    //Interprets UCI commands and calls the engine's functions 
    public void readCommand(string message)
    {
        writeCommandToLog(message);
        message = message.Trim();
        string messageType = message.Split(' ')[0].ToLower();

        //This switch statement handles the necessary UCI commands for a chess GUI to interact with the bot
        switch (messageType)
        {
            case "uci":
                writeToConsole("id name HotHandEngine");
                writeToConsole("id author HotHandSystems");
                writeToConsole("uciok");
                break;
            case "isready":
                writeToConsole("readyok");
                break;
            case "ucinewgame":
                //IMPLEMENT ENGINE CLASS

                //Call the newGame function from the engine class and then respond to console with "readyok"

                break;
            case "position":
                chessEngine.loadPosition(message);
                break;
            case "go":
                writeToConsole("bestmove d7d5");
                //HARDCODED AS A TEST - FIX LATER
                break;
            case "stop":
                //IMPLEMENT ENGINE CLASS
                break;
            case "quit":
                //IMPLEMENT ENGINE CLASS

                //Likely needs to do nothing
                break;
            case "d":
                chessEngine.printPosition();
                break;
            default:
                writeToLog($"Bad Command: {messageType}");
                break;
        }
    }

    //Writes a response to the console
    public void writeToConsole(string response)
    {
        Console.WriteLine(response);
        writeToLog($"{engineName}: {response}");
    }

    public void writeCommandToLog(string message)
    {
        writeToLog($"GUI: {message}");
    }

    //Writes text as a line in the log
    public void writeToLog(string text)
    {
        if (logInteraction)
        {
            Directory.CreateDirectory(logPath);
            string path = Path.Combine(logPath, logFileName);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(text);
            }
        }

        return;
    }

}