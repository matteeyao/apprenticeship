using NUnit.Framework;
using src.Players.Strategies;
using System;
using System.Collections.Generic;

namespace test.Players.Strategies
{
    [TestFixture]
    public class MinimaxTest
    {
        [Test]
        public void MinimaxReturnsNegativeScoreIfOpponentCanWin()
        {
            string[] fields =
            {
                "o", "o", "x",
                "x", "x", "5",
                "o", "o", "8"
            };
            int bestValue = Minimax.MinOrMaxScore(fields, "o", 3, true, 3);
            Assert.AreEqual(-20, bestValue);
        }
        
        [Test]
        public void MinimaxReturnsPositiveScoreIfWinningBoard()
        {
            string[] fields = {
                "o", "o", "o",
                "3", "4", "5",
                "6", "7", "8"
            };
            int bestValue = Minimax.MinOrMaxScore(fields, "o", 7, true, 7);
            Assert.AreEqual(70, bestValue);
        }
        
        [Test]
        public void MinimaxReturnsPositiveScoreIfInFavorablePosition()
        {
            string[] fields = {
                "o", "1", "o",
                "x", "x", "o",
                "6", "7", "8" 
            };
            int bestValue = Minimax.MinOrMaxScore(fields, "o", 5, true, 5);
            Assert.AreEqual(30, bestValue);
        }
        
        [Test]
        public void MinimaxReturnZeroScoreIfBestItCanDoIsTie()
        {
            string[] fields = {
                "o", "x", "o",
                "x", "x", "5",
                "x", "o", "8"
            };
            int bestValue = Minimax.MinOrMaxScore(fields, "x", 3, false, 3);
            Assert.AreEqual(0, bestValue);
        }
        
        [Test]
        public void ReturnsImmediateChildrenOfABoard()
        {
            List<string[]> children = new List<string[]>();

            string[] fields = {
                "x", "o", "x",
                "o", "x", "o",
                "6", "x", "8"
            };

            string[] childOne = {
                "x", "o", "x",
                "o", "x", "o",
                "o", "x", "8"
            };
            
            string[] childTwo = {
                "x", "o", "x",
                "o", "x", "o",
                "6", "x", "o"
            };

            children.Add(childOne);
            children.Add(childTwo);

            Assert.AreEqual(children, Minimax.FindNextBoards(fields, "o"));
        }
        
        [Test]
        public void CanFindTheImmediateChildren()
        {
            string[] fields = {
                "0", "1", "2",
                "3", "4", "5",
                "6", "7", "8"
            };
            
            List<string[]> children = Minimax.FindNextBoards(fields, "x");

            foreach(string field in fields)
            {
                int index = Int32.Parse(field);
                string[] child = children[index];

                Assert.AreEqual("x", child[index]);
            }
        }
    }
}