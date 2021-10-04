using NUnit.Framework;
using src.UI;

namespace test.UI
{
    [TestFixture]
    public class ValidatorTest
    {
        [Test]
        public void ReturnsTrueIfMarkerIsValid()
        {
            Assert.IsTrue(Validator.IsMarkerValid("x"));
        }
        
        [Test]
        public void ReturnsFalseIfMarkerIsNotValid()
        {
            Assert.IsFalse(Validator.IsMarkerValid("p"));
        }
        
        [Test]
        public void ReturnsTrueIfTurnOrderIsValid()
        {
            Assert.IsTrue(Validator.IsTurnOrderValid("1"));
            Assert.IsTrue(Validator.IsTurnOrderValid("2"));
        }

        [Test]
        public void ReturnFalseIfTurnOrderIsInvalid()
        {
            Assert.IsFalse(Validator.IsTurnOrderValid("3"));
            Assert.IsFalse(Validator.IsTurnOrderValid("Q"));
        }
        
        [Test]
        public void ReturnsTrueIfFieldIsAvailable()
        {
            string[] fields = { "0", "X", "2", "3", "O", "5", "6", "7", "8" };
            Assert.IsTrue(Validator.IsMoveValid("0", fields));
        }

        [Test]
        public void ReturnsFalseIfFieldIsUnavailable()
        {
            string[] fields = { "0", "x", "2", "3", "O", "5", "6", "7", "8" };
            Assert.IsFalse(Validator.IsMoveValid("1", fields));
        }

        [Test]
        public void ReturnFalseIfFieldIsInvalid()
        {
            string[] fields = { "0", "x", "2", "3", "O", "5", "6", "7", "8" };
            Assert.IsFalse(Validator.IsMoveValid("9", fields));
        }
        
        [Test]
        public void ReturnsTrueIfValidGameMode()
        {
            Assert.IsTrue(Validator.IsGameModeValid("hc"));
        }

        [Test]
        public void ReturnsFalseIfInvalidGameMode()
        {
            Assert.IsFalse(Validator.IsGameModeValid("pp"));
        }
        
        [Test]
        public void ReturnsTrueForHumanVsHumanGameMode()
        {
            Assert.IsTrue(Validator.IsGameModeValid("hh"));
        }

        [Test]
        public void ReturnsTrueIfValidRegardlessOfCapitalzation()
        {
            Assert.IsTrue(Validator.IsGameModeValid("HC"));
        }

        [Test]
        public void ReturnsTrueIfValidStrategyLevel()
        {
            Assert.IsTrue(Validator.StrategyLevel("E"));
        }
        
        [Test]
        public void ReturnsTrueIfValidBoardDimensions()
        {
            Assert.IsTrue(Validator.IsBoardDimensionValid("3"));
        }

        [Test]
        public void ReturnFalseIfNotValidBoardDimensions()
        {
            Assert.IsFalse(Validator.IsBoardDimensionValid("5"));
        }
    }
}