using NUnit.Framework;
using System;
using src;
using src.GameLogic;

namespace test.GameLogic
{
    [TestFixture]
    public class BoardEvaluatorTest
    {
        [Test]
        public void FieldThatReturnsAMarkerIsNotEmpty()
        {
            string field = "x";
            bool result = BoardEvaluator.IsEmptyField(field);
            Assert.IsFalse(result);
        }

        [Test]
        public void FieldThatReturnsANoneMarkerIsEmpty()
        {
            string field = "4";
            bool result = BoardEvaluator.IsEmptyField(field);
            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnsTrueIfAllFieldsAreNotEmpty()
        {
            string[] fields = { "x", "o", "x", "x", "x", "x", "x", "x", "x" };
            bool result = BoardEvaluator.IsBoardFilled(fields);
            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnsFaleIfNotAllFieldsAreEmpty()
        {
            string[] fields = { "x", "o", "x", "o", "o", "x", "o", "x", "8" };
            bool result = BoardEvaluator.IsBoardFilled(fields);
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnsMatrixOfRowColumnAndDiagonalSequences()
        {
            string[] spaces = {"0", "1", "2",
                "3", "4", "5",
                "6", "7", "8"};
            string[][] rowsColumnsDiagonals = new string[8][];
            rowsColumnsDiagonals[0] = new string[] { "0", "1", "2" };
            rowsColumnsDiagonals[1] = new string[] { "3", "4", "5" };
            rowsColumnsDiagonals[2] = new string[] { "6", "7", "8" };
            rowsColumnsDiagonals[3] = new string[] { "0", "3", "6" };
            rowsColumnsDiagonals[4] = new string[] { "1", "4", "7" };
            rowsColumnsDiagonals[5] = new string[] { "2", "5", "8" };
            rowsColumnsDiagonals[6] = new string[] { "0", "4", "8" };
            rowsColumnsDiagonals[7] = new string[] { "2", "4", "6" };
            CollectionAssert.AreEqual(rowsColumnsDiagonals, 
                BoardEvaluator.GenerateRowColumnAndDiagonalSequences(spaces));
        }

        [Test]
        public void ReturnsMatrixOfRowColumnAndDiagonalSequencesOfAnySizeBoard()
        {
            string[] fields = {"0", "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10", "11",
                "12", "13", "14", "15"};

            string[][] sequences = new String[10][];
            sequences[0] = new string[] { "0", "1", "2", "3" };
            sequences[1] = new string[] { "4", "5", "6", "7" };
            sequences[2] = new string[] { "8", "9", "10", "11" };
            sequences[3] = new string[] { "12", "13", "14", "15" };
            sequences[4] = new string[] { "0", "4", "8", "12" };
            sequences[5] = new string[] { "1", "5", "9", "13" };
            sequences[6] = new string[] { "2", "6", "10", "14" };
            sequences[7] = new string[] { "3", "7", "11", "15" };
            sequences[8] = new string[] { "0", "5", "10", "15" };
            sequences[9] = new string[] { "3", "6", "9", "12" };

            CollectionAssert.AreEqual(sequences, BoardEvaluator.GenerateRowColumnAndDiagonalSequences(fields));
        }

        [Test]
        public void ReturnsTrueIfAnySetsAreTheSame()
        {
            string[] fields = { "x", "x", "x", "3", "4", "5", "6", "7", "8" };
            Assert.IsTrue(BoardEvaluator.IsAnySequenceUniform(fields));
        }

        [Test]
        public void ReturnsFalseIfNoSetsAreTheSame()
        {
            string[] spaces = { "x", "x", "2", "3", "4", "5", "6", "7", "8" };
            Assert.IsFalse(BoardEvaluator.IsAnySequenceUniform(spaces));
        }

        [Test]
        public void ReturnsAvailableFields()
        {
            string[] fields = { "x", "x", "2", "3", "4", "5", "6", "7", "8" };
            string[] expected = { "2", "3", "4", "5", "6", "7", "8" };
            Assert.AreEqual(expected, BoardEvaluator.FilterEmptyFields(fields));
        }
    }
}