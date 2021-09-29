using System;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string num = "o";
            Int32.TryParse(num, out int val);
            Console.Write(val);
            // Board board = new Board(3);

            // board.SetField(2, "x");
            // board.SetField(4, "x");
            // board.SetField(7, "x");
            //
            // string winner = board.Winner();
            // Console.WriteLine(winner);

            // int rowLength = sequences.GetLength(0);
            // int colLength = sequences.GetLength(1);
            //
            // for (int i = 0; i < rowLength; i++)
            // {
            //     for (int j = 0; j < colLength; j++)
            //     {
            //         Console.Write(string.Format("{0} ", sequences[i, j]));
            //     }
            //     Console.Write(Environment.NewLine + Environment.NewLine);
            // }

        }
    }
}