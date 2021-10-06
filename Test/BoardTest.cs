using App;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class BoardTest
    {
        private Board board;
        
        [SetUp]
        public void Setup()
        {
            board = new Board(Board.Dimensions.ThreeByThree);
        }

        [Test]
        public void FieldsAreEmptyUponInitialization()
        {
            string[] emptyBoard = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Assert.AreEqual(emptyBoard, board.GetGrid());
        }

        [Test]
        public void PlacesMarkOnField()
        {
            board.SetField(4, "x");
            Assert.AreEqual("x", board.GetField(4));
        }

        [Test]
        public void AcknowledgesFieldAlreadyHasMark()
        {
            Assert.AreEqual(true, board.IsEmptyField(4));
            
            board.SetField(4, "x");
            Assert.AreEqual(false, board.IsEmptyField(4));
        }

        [Test]
        public void ReturnsDuplicateOfCurrentBoard()
        {
            Board dupedBoard = board.Duplicate();
            Assert.AreNotEqual(dupedBoard, board);
        }

        [Test]
        public void IdentifiesRowWinningMark()
        {
            string[] grid = new string[9] {"x", "x", "x", "4", "5", "6", "7", "8", "9"};
            Board firstBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual("x", firstBoard.Winner());
            
            grid = new string[9] {"1", "2", "3", "x", "x", "x", "7", "8", "9"};
            Board secondBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual("x", secondBoard.Winner());
            
            grid = new string[9] {"1", "2", "3", "4", "5", "6", "o", "o", "o"};
            Board thirdBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual("o", thirdBoard.Winner());
        }
        
        [Test]
        public void IdentifiesColumnWinningMark()
        {
            string[] grid = new string[9] {"x", "2", "3", "x", "5", "6", "x", "8", "9"};
            Board firstBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual("x", firstBoard.Winner());
            
            grid = new string[9] {"1", "x", "3", "4", "x", "6", "7", "x", "9"};
            Board secondBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual("x", secondBoard.Winner());
            
            grid = new string[9] {"1", "2", "o", "4", "5", "o", "7", "8", "o"};
            Board thirdBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual("o", thirdBoard.Winner());
        }
        
        [Test]
        public void IdentifiesDiagonalWinningMark()
        {
            string[] grid = new string[9] {"x", "2", "3", "4", "x", "6", "7", "8", "x"};
            Board firstBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual("x", firstBoard.Winner());
            
            grid = new string[9] {"1", "2", "o", "4", "o", "6", "o", "8", "9"};
            Board secondBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual("o", secondBoard.Winner());
        }

        [Test]
        public void IdentifiesDraw()
        {
            string[] grid = new string[9] {"x", "x", "o", "o", "o", "x", "x", "o", "x"};
            Board firstBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual(true, firstBoard.IsTied());
            
            grid = new string[9] {"x", "x", "x", "o", "o", "x", "x", "o", "o"};
            Board secondBoard = new Board(Board.Dimensions.ThreeByThree, grid);
            Assert.AreEqual(false, secondBoard.IsTied());
        }
    }
}
