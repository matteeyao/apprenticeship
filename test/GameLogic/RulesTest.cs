using NUnit.Framework;
using src.GameLogic;

namespace test.GameLogic
{
    [TestFixture]
    public class RulesTest
    {
        [Test]
        public void ReturnsTrueWhenBoardIsFilledButThereIsNoWinner()
        {
            string[] fields = { 
                "x", "o", "x",
                "x", "x", "o",
                "o", "x", "o" 
            };
            Assert.IsTrue(Rules.IsOver(fields));
        }
        
        [Test]
        public void ReturnsFalseWhenBoardIsEmpty()
        {
            string[] fields = { 
                "0", "1", "2",
                "3", "4", "5",
                "6", "7", "8" 
            };
            Assert.IsFalse(Rules.IsOver(fields));
        }
        
        [Test]
        public void ReturnsTrueWhenGameIsWon()
        {
            string[] fields = { 
                "x", "x", "x",
                "3", "4", "5",
                "6", "7", "8"
            };
            Assert.IsTrue(Rules.IsOver(fields));
        }
        
        [Test]
        public void ReturnsTrueWhenAMarkerHasFilledTopRow()
        {
            string[] fields = { 
                "x", "x", "x",
                "3", "4", "5",
                "6", "7", "8" 
            };
            Assert.IsTrue(Rules.IsWon(fields));
        }
        
        [Test]
        public void ReturnsFalseWhenGameIsNotWonAndTopRowIsFilledWithDifferentMarkers()
        {
            string[] fields = { 
                "x", "o", "x",
                "3", "4", "5",
                "6", "7", "8" 
            };
            Assert.IsFalse(Rules.IsWon(fields));
        }
        
        [Test]
        public void ReturnsTrueWhenGameIsNotWonAndSecondRowIsFilledWithOneMarker()
        {
            string[] fields = { 
                "0", "1", "2", 
                "x", "x", "x", 
                "6", "7", "8" 
            };
            Assert.IsTrue(Rules.IsWon(fields));
        }
        
        [Test]
        public void ReturnsTrueWhenGameIsWonAndThirdRowIsFilledWithOneMarker()
        {
            string[] fields = { 
                "0", "1", "2",
                "3", "4", "5", 
                "x", "x", "x"
            };
            Assert.IsTrue(Rules.IsWon(fields));
        }
        
        [Test]
        public void ReturnsTrueWhenGameIsWonAndFirstColumnIsFilledWithOneMarker()
        {
            string[] fields = {
                "o", "1", "2",
                "o", "4", "5",
                "o", "7", "8"
            };
            Assert.IsTrue(Rules.IsWon(fields));
        }
        
        [Test]
        public void ReturnsTrueWhenGameIsWonAndSecondColumnIsFilledWithOneMarker()
        {
            string[] fields = {
                "0", "x", "2",
                "3", "x", "5",
                "6", "x", "8"
            };
            Assert.IsTrue(Rules.IsWon(fields));
        }
        
        [Test]
        public void ReturnsTrueWhenGameIsWonAndThirdColumnIsFilledWithOneMarker()
        {
            string[] fields = {
                "0", "1", "x",
                "3", "4", "x",
                "6", "7", "x"
            };
            Assert.IsTrue(Rules.IsWon(fields));
        }

        [Test]
        public void ReturnsTrueWhenGameIsWonAndFirstDiagonalIsFilledWithOneMarker()
        {
            string[] fields = {
                "x", "1", "2",
                "3", "x", "5",
                "6", "7", "x"
            };
            Assert.IsTrue(Rules.IsWon(fields)); 
        }
        
        [Test]
        public void ReturnsTrueWhenGameIsWonAndSecondDiagonalIsFilledWithOneMarker()
        {
            string[] fields = {
                "0", "1", "x",
                "3", "x", "5",
                "x", "7", "8"
            };
            Assert.IsTrue(Rules.IsWon(fields));
        }
    }
}