using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Board
    {
        public enum Marks { x, o };

        public enum Dimensions
        {
            ThreeByThree=3,
            FourByFour=4,
            FiveByFive=5
        };
        private readonly string[] grid;
        private readonly int dimension;

        public Board(Dimensions dimension, string[] grid = null)
        {
            this.dimension = (int) dimension;
            this.grid = grid ?? GenerateGrid((int) dimension);
        }

        public string[] GetGrid()
        {
            return this.grid;
        }

        public string GetField(int position)
        {
            return this.grid[position];
        }

        public void SetField(int position, string mark)
        {
            this.grid[position] = mark;
        }

        public bool IsValidField(int position)
        {
            int numFields = (int)Math.Pow(this.dimension, 2);
            return !(position < 0 || numFields < position);
        }

        public bool IsEmptyField(int position)
        {
            // return !(this.GetField(position).Equals(Board.Marks.x) || this.GetField(position).Equals("o"));
            return !Enum.IsDefined(typeof(Marks), this.GetField(position));
        }
        
        public Board Duplicate()
        {
            string[] dupedGrid = (string[]) this.grid.Clone();
            return new Board((Dimensions) Enum.ToObject(typeof(Dimensions), this.dimension), dupedGrid);
        }

        public string Winner()
        {
            string[,] rows = this.GetRows();
            string[,] cols = this.GetColumns();
            string[,] diags = this.GetDiagonals();

            return WinnerHelper(rows) ?? WinnerHelper(cols) ?? WinnerHelper(diags);
        }

        private string WinnerHelper(string[,] sequences)
        {
            int rowLength = sequences.GetLength(0);
            int colLength = sequences.GetLength(1);
            for (int rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                string[] sequence = new string[colLength]; 
                for (int colIdx = 0; colIdx < colLength; colIdx++)
                {
                    sequence[colIdx] = sequences[rowIdx, colIdx];
                }

                if (sequence.Distinct().Count() == 1)
                {
                    return sequence[0];
                }
            }

            return null;
        }
        
        public bool HasWinner()
        {
            return this.Winner() != null;
        }

        public bool IsTied()
        {
            if (this.HasWinner())
            {
                return false;
            }
        
            foreach (string field in this.grid)
            {
                Int32.TryParse(field, out int val);
                if (val != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private sealed class GridDimensionEqualityComparer : IEqualityComparer<Board>
        {
            public bool Equals(Board x, Board y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x.grid, y.grid) && x.dimension == y.dimension;
            }

            public int GetHashCode(Board obj)
            {
                return HashCode.Combine(obj.grid, obj.dimension);
            }
        }

        public static IEqualityComparer<Board> GridDimensionComparer { get; } = new GridDimensionEqualityComparer();

        private static string[] GenerateGrid(int dimension)
        {
            int numFields = (int)Math.Pow(dimension, 2);
            return Enumerable.Range(1, numFields).Select(i => i.ToString()).ToArray();
        }

        private string[,] GetRows()
        {
            int dim = this.dimension;
            string[,] rows = new string[dim, dim];
            for (int i = 0; i < this.grid.Length; i++)
            {
                rows[i / dim, i % dim] = this.grid[i];
            }

            return rows;
        }
        
        private string[,] GetColumns()
        {
            int dim = this.dimension;
            string[,] cols = new string[dim, dim];
            for (int i = 0; i < this.grid.Length; i++)
            {
                cols[i % dim, i / dim] = this.grid[i];
            }

            return cols;
        }
        
        private string[,] GetDiagonals()
        {
            int dim = this.dimension;
            string[,] diags = new string[2, dim];
            for (int i = 0; i < this.grid.Length; i += (dim + 1))
            {
                diags[0, i % dim] = this.grid[i];
            }

            for (int i = dim - 1; i < this.grid.Length - 1; i += (dim - 1))
            {
                diags[1, i / dim] = this.grid[i];
            }

            return diags;
        }
    }
}
