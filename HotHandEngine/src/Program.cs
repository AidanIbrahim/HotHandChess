using Chess.Core;
using System;
using System;
namespace HotHandEngine
 
{
    internal class Program
    {
        static void Main(string[] args)
        {

            UCIIntepreter engine = new();
            string command = String.Empty;

            //While the command is not "quit" keep reading inputs
            while (command != "quit")
            {
                if (command == null)
                {
                    break;
                }
                command = Console.ReadLine();
                engine.readCommand(command);
            }
        }
    }
}
