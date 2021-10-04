using NUnit.Framework;
using src.UI;

namespace test.UI
{
    [TestFixture]
    public class PromptTest
    {
        [Test]
        public void ReturnsAName()
        {
            TestHelper.TestHelper.SetInput("Kirby\n");
            string name = Prompt.GetPlayerName();
            Assert.AreEqual("Kirby", name);
        }

        [Test]
        public void ReturnsAMarker()
        {
            TestHelper.TestHelper.SetInput("x\n");
            string marker = Prompt.GetInput(MessageHandler.InquireForMarker, Validator.IsMarkerValid);
            Assert.AreEqual("x", marker);
        }
        
        [Test]
        public void AsksForMarkerAgainIfInvalid()
        {
            TestHelper.TestHelper.SetInput("p\no\n");
            string marker = Prompt.GetInput(MessageHandler.InquireForMarker, Validator.IsMarkerValid);
            Assert.AreNotEqual("p", marker);
            Assert.AreEqual("o", marker);
        }
        
        [Test]
        public void ReturnsAMove()
        {
            TestHelper.TestHelper.SetInput("4\n");
            string[] fields = { "0", "1", "2", "3", "4", "5", "6", "7", "8" };
            int move = Prompt.GetPlayerMove("Bob", fields);
            Assert.AreEqual(4, move);
        }
        
        [Test]
        public void GetTurnOrder()
        {
            TestHelper.TestHelper.SetInput("1\n");
            string turnOrder = Prompt.GetTurnOrder("Bob");
            Assert.AreEqual("1", turnOrder);
        }
        
        [Test]
        public void DoesNotAcceptAnInvalid_Turn_Order()
        {
            TestHelper.TestHelper.SetInput("3\n2\n");
            string turnOrder = Prompt.GetTurnOrder("Robert");
            Assert.AreNotEqual("3", turnOrder);
            Assert.AreEqual("2", turnOrder);
        }
        
        [Test]
        public void DoesNotAcceptInvalidMove()
        {
            TestHelper.TestHelper.SetInput("fake move!\n2\n");
            string[] fields = { "0", "1", "2", "3", "4", "5", "6", "7", "8" };
            int move = Prompt.GetPlayerMove("Robert", fields);
            Assert.AreEqual(2, move);
            Assert.AreNotEqual("fake move", move);
        }

        [Test]
        public void ReturnsAGameModeIfValid()
        {
            TestHelper.TestHelper.SetInput("hc\n");
            Assert.AreEqual("hc", Prompt.GetInput(MessageHandler.AskPlayerForGameMode, Validator.IsGameModeValid));
        }
        
        [Test]
        public void ReturnsAStrategyLevel()
        {
            TestHelper.TestHelper.SetInput("e\n");
            Assert.AreEqual("e", Prompt.GetInput(MessageHandler.AskPlayerForDifficultyLevel, Validator.StrategyLevel));
        }
        
        [Test]
        public void ReturnsValidInputIfValidGameModes()
        {
            TestHelper.TestHelper.SetInput("fun game mode\nhh\n");
            string input = Prompt.GetInput(MessageHandler.AskPlayerForGameMode, Validator.IsGameModeValid); 
            Assert.AreNotEqual("fun game mode", input);
            Assert.AreEqual("hh", input);
        }

        [Test]
        public void ReturnsValidInputIfValidTurnOrder()
        {
            TestHelper.TestHelper.SetInput("80th\n1\n");
            string input = Prompt.GetInput(MessageHandler.AskPlayerForGameMode, Validator.IsTurnOrderValid);
            Assert.AreNotEqual("80th", input);
            Assert.AreEqual("1", input);
        }
    }
}