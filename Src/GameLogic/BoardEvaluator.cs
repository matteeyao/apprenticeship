using System;
using System.Collections.Generic;
using System.Linq;

namespace src.GameLogic
{
    public class BoardEvaluator
    {
        public static bool IsEmptyField(string field)
        {
            return field != GlobalConstants.GlobalConstants.XMarker && field != GlobalConstants.GlobalConstants.OMarker;
        }

        public static bool IsBoardFilled(string[] fields)
        {
            return fields.All(field => !IsEmptyField(field));
        }

        public static string[] FilterEmptyFields(string[] fields)
        {
            return fields.Where(field => IsEmptyField(field)).ToArray();
        }

        public static bool IsAnySequenceUniform(string[] fields)
        {
            return GenerateRowColumnAndDiagonalSequences(fields).Any(sequence => IsSequenceUniform(sequence));
        }

        public static string[][] GenerateRowColumnAndDiagonalSequences(string[] fields)
        {
            int boardDimension = GetBoardDimension(fields);
            int numberOfSequences = boardDimension * 2 + 2;

            string[][] sequences = new String[numberOfSequences][];
            Rows(fields).CopyTo(sequences, 0);
            Columns(fields).CopyTo(sequences, boardDimension);
            Diagonals(fields).CopyTo(sequences, boardDimension * 2);
            return sequences;
        }

        public static bool IsSequenceUniform(string[] fields)
        {
            return fields.Distinct().Count() == 1;
        }

        public static string[][] Rows(string[] fields)
        {
            int boardDimension = GetBoardDimension(fields);
            string[][] rows = new string[boardDimension][];

            for (int i = 0; i < boardDimension; i++)
            {
                string[] row = fields.SubArray(i * boardDimension, boardDimension);
                rows[i] = row;
            }

            return rows;
        }

        private static string[][] Columns(string[] fields)
        {
            int boardDimension = GetBoardDimension(fields);
            string[][] columns = new string[boardDimension][];

            for (int i = 0; i < boardDimension; i++)
            {
                string[] column = new string[boardDimension];
                for (int j = 0; j < boardDimension; j++)
                {
                    int index = i + (j * boardDimension);
                    column[j] = fields[index];
                }

                columns[i] = column;
            }

            return columns;
        }

        private static string[][] Diagonals(string[] fields)
        {
            string[][] diagonals = new string[2][];
            int boardDimension = GetBoardDimension(fields);

            diagonals[0] = FirstDiagonal(fields);
            diagonals[1] = SecondDiagonal(fields);

            return diagonals;
        }

        public static int GetBoardDimension(string[] fields)
        {
            double squareRootOfNumBoardFields = Math.Sqrt(fields.Length);
            return Convert.ToInt32(squareRootOfNumBoardFields);
        }
        
        private static string[] FirstDiagonal(string[] fields)
        {
            int boardDimension = GetBoardDimension(fields);
            int numberOfSpacesBetweenNextIndex = boardDimension + 1;
            string[] diagonal = new string[boardDimension];

            for(int j = 0; j < boardDimension; j += 1)
            {
                int index = j * numberOfSpacesBetweenNextIndex;
                diagonal[j] = fields[index];
            }

            return diagonal;
        }
        private static string[] SecondDiagonal(string[] fields)
        {
            int boardDimension = GetBoardDimension(fields);
            int numberOfSpacesBetweenNextIndex = boardDimension - 1;
            string[] diagonal = new string[boardDimension];

            for(int j = 0; j < boardDimension; j += 1)
            {
                int index = numberOfSpacesBetweenNextIndex + (j * numberOfSpacesBetweenNextIndex);
                diagonal[j] = fields[index];
            }

            return diagonal;
        }
    }
}