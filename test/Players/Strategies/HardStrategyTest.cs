using NUnit.Framework;
using src.Players.Strategies;
using System.Diagnostics;

namespace test.Players.Strategies
{
    [TestFixture]
    public class HardStrategyTest
    {
        HardStrategy hardStrategy;

        [SetUp]
        public void NewHardStrategyObjectIsCreated()
        {
            hardStrategy = new HardStrategy();
        }

        [Test]
        public void BestMoveReturnsTheWinningMove()
        {
            string[] fields = {
                "o", "o", "2",
                "3", "4", "5",
                "6", "7", "8"
            };
            Assert.AreEqual(2, hardStrategy.BestMove(fields, "o"));
        }
        
        [Test]
        public void BestMoveReturnsBlockingMove()
        {
            string[] fields = {
                "0", "x", "2",
                "o", "x", "5",
                "6", "7", "8"
            };
            Assert.AreEqual(7, hardStrategy.BestMove(fields, "o"));
        }
        
        [Test]
        public void BestMoveEnsuresATie()
        {
            string[] fields = {
                "o", "x", "o",
                "x", "x", "5",
                "x", "o", "8"
            };
            Assert.AreEqual(5, hardStrategy.BestMove(fields, "o"));
        }
        
        [Test]
        public void CanFindBestMoveUnderASecondWhenMovingFirst()
        {
            var watch = new Stopwatch();
            string[] fields = { "0", "1", "2", "3", "4", "5", "6", "7", "8" };

            watch.Start();
            hardStrategy.BestMove(fields, "x");
            watch.Stop();

            Assert.IsTrue(watch.Elapsed.TotalMilliseconds < 1000);
        }
    }
}