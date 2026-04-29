using Chess.Core;
using System;
using System;
namespace HotHandEngine
 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, HotHand Systems!");

            UCIIntepreter engine = new();
            string command = String.Empty;
            while (command != "quit")
            {
                command = Console.ReadLine();
                engine.readCommand(command);
            }
        }
    }
}
