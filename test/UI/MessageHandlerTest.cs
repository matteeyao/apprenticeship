using NUnit.Framework;
using System;
using System.IO;
using src.UI;

namespace test.UI
{
    [TestFixture]
    public class MessageHandlerTest
    {
        public StringWriter CaptureTheOuput()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            return sw;
        }

        [Test]
        public void ConsoleAsksForName()
        {
            StringWriter sw = CaptureTheOuput();
            MessageHandler.InquireForName();
            StringAssert.Contains("What is your name?", sw.ToString());
        }
        
        [Test]
        public void ConsoleAsksForMove()
        {
            StringWriter sw = CaptureTheOuput();
            MessageHandler.InquireForMove("Robert");
            StringAssert.Contains("Where would you like to move Robert?", sw.ToString());
        }

        [Test]
        public void ConsoleAsksForMarker()
        {
            StringWriter sw = CaptureTheOuput();
            MessageHandler.InquireForMarker();
            StringAssert.Contains("What marker would you like to be, x or o?", sw.ToString());
        }

        [Test]
        public void ConsoleAsksForTurnOrder()
        {
            StringWriter sw = CaptureTheOuput();
            MessageHandler.InquireForTurnOrder("Tony");
            string expected = "Type 1 if you would like Tony to go first, and 2 to go second.";
            StringAssert.Contains(expected, sw.ToString());
        }

        [Test]
        public void ConsolePrintsBoard()
        {
            StringWriter sw = CaptureTheOuput();
            string[] fields = { "0", "1", "2", "3", "4", "5", "6", "7", "8" };
            MessageHandler.PrintBoard(fields);
            string expected =
                @"0 |  1 |  2 |";
            StringAssert.Contains(expected, sw.ToString());
        }
        
        [Test]
        public void ConsolePrintsAPartiallyFilledBoard()
        {
            StringWriter sw = CaptureTheOuput();
            string[] spaces= { "x", "O", "2", "3", "4", "5", "6", "7", "8" };
            MessageHandler.PrintBoard(spaces);
            string expected =
                @"x |  O |  2";
            StringAssert.Contains(expected, sw.ToString());
        }
        
        [Test]
        public void ConsolePrintsAnySizeBoard()
        {
            StringWriter sw = CaptureTheOuput();
            string[] spaces= { 
                "0", "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10", "11",
                "12", "13", "14", "15" 
            };

            MessageHandler.PrintBoard(spaces);
            string expected =
                @"  0 |  1 |  2 |  3 |
                    4 |  5 |  6 |  7 |
                    8 |  9 |  10 |  11 |
                    12 |  13 |  14 |  15 |";
                        
            StringAssert.Contains("15", expected) ;
        }

        [Test]
        public void ConsolePrintsAMessageForTheWinner()
        {
            StringWriter sw = CaptureTheOuput();
            string winnersMessage = "Robert has won the game";
            MessageHandler.Winner("Robert");
            StringAssert.Contains(winnersMessage, sw.ToString());
        }
        
        [Test]
        public void ConsolePrintsAMessageForATieGame()
        {
            StringWriter sw = CaptureTheOuput();
            string tiedMessage = "The game is a tie";
            MessageHandler.Tied();
            StringAssert.Contains(tiedMessage, sw.ToString()); 
        }
        
        [Test]
        public void ConsolePrintsAMessageForInvalidInput()
        {
            StringWriter sw = CaptureTheOuput();
            string invalidMessage = "P is not a valid input";
            MessageHandler.Invalid("P");
            StringAssert.Contains(invalidMessage, sw.ToString());
        }
        
        [Test]
        public void ConsolePrintsAMessageForDifficulty()
        {
            StringWriter sw = CaptureTheOuput();
            string message = "Type in E for easy or H for hard difficulty";
            MessageHandler.AskPlayerForDifficultyLevel();
            StringAssert.Contains(message, sw.ToString());
        }
        
        [Test]
        public void MessageForDimmensionsOfBoard()
        {
            StringWriter sw = CaptureTheOuput();
            string message = "Type in 3 to play on a 3 by 3 board, or 4 for a 4 by 4 board";
            MessageHandler.AskPlayerForBoardDimmensions();
            StringAssert.Contains(message, sw.ToString());
        }
    }
}