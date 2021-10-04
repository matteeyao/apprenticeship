using System;
using src.GameLogic;


namespace src.UI
{
    public class MessageHandler
    {
        public static void InquireForName()
        {
            Console.WriteLine("What is your name?");    
        }

        public static void InquireForMove(string name)
        {
            Console.WriteLine($"Where would you like to move {name}?");
        }

        public static void InquireForMarker()
        {
            Console.WriteLine("What marker would you like to be, x or o?");
        }

        public static void InquireForTurnOrder(string name)
        {
            Console.WriteLine($"Type 1 if you would like {name} to go first, and 2 to go second.");
        }

        public static void PrintBoard(string[] fields)
        {
            string board = "";
            string[][] rows = BoardEvaluator.Rows(fields);
            int boardDimension = BoardEvaluator.GetBoardDimension(fields);

            for (int currentRowIndex = 0; currentRowIndex < rows.Length; currentRowIndex++)
            {
                string row = "";
                for (int currentColumnIndex = 0; currentColumnIndex < rows.Length; currentColumnIndex++)
                {
                    int index = currentRowIndex * boardDimension + currentColumnIndex;
                    row = string.Format(@"{0} {1} | ", row, fields[index]);
                }

                board = string.Format(@"{0}
    
                                        {1}", board, row);
            }
            
            Console.WriteLine(board);
        }
        
        public static void Winner(string name)
        {
            Console.WriteLine(name + " has won the game");
        }

        public static void Tied()
        {
            Console.WriteLine("The game is a tie");
        }

        public static void Invalid(string input)
        {
            Console.WriteLine(input + " is not a valid input");
        }

        public static void AskPlayerForGameMode()
        {
            Console.WriteLine("Type in hh to play human vs human, and hc for human vs computer");
        }

        public static void AskPlayerForDifficultyLevel()
        {
            Console.WriteLine("Type in E for easy or H for hard difficulty");
        }

        public static string ReadInput()
        {
            return Console.ReadLine();
        }

        public static void AskPlayerForBoardDimmensions()
        {
            Console.WriteLine("Type in 3 to play on a 3 by 3 board, or 4 for a 4 by 4 board");
        }
    }
}