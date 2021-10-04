using NUnit.Framework;
using src.Players.Strategies;

namespace test.Players.Strategies
{
    [TestFixture]
    public class EasyStrategyTest
    {
        public EasyStrategy easyStrategy;

        [SetUp]
        public void ANewEasyStrategy()
        {
            easyStrategy = new EasyStrategy();
        }

        [Test]
        public void MakesARandomMove()
        {
            string[] fields =
            {
                "x", "x", "o",
                "o", "o", "o",
                "6", "7", "x"
            };
            string randomMove = easyStrategy.RandomMove(fields);
            Assert.IsTrue("7" == randomMove || "6" == randomMove);
        }

        [Test]
        public void BestMoveIsWinningMoveIfAvailable()
        {
            string[] fields = { 
                "x", "x", "o",
                "o", "o", "5",
                "6", "7", "x" 
            };
            Assert.AreEqual(5, easyStrategy.BestMove(fields, "o"));
        }
        
        [Test]
        public void BestMoveIsAWinningMoveIfAvailable()
        {
            string[] fields = {
                "x", "x", "2", "x",
                "4", "5", "6", "7",
                "8", "9", "10", "11",
                "12", "13", "14", "15"
            };

            Assert.AreEqual(2, easyStrategy.BestMove(fields, "x"));
        }
        
        [Test]
        public void ReturnsRandomMoveIfNoWinningMoveAvailable()
        {
            string[] spaces = {
                "x", "1", "o",
                "o", "o", "x",
                "x", "7", "8"
            };
            int bestMove = easyStrategy.BestMove(spaces, "x");
            Assert.IsTrue(1 == bestMove || 7 == bestMove || 8 == bestMove);
        }
    }
}