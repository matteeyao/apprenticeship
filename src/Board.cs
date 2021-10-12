using System;
using System.Collections.Generic;
using System.Linq;

namespace src
{
    public class Board
    {
        private readonly string[] _grid;
        private readonly int _dimension;

        public Board(int dimension, string[] grid = null)
        {
            this._dimension = dimension;
            this._grid = grid ?? GenerateGrid(dimension);
        }

        public string[] GetGrid()
        {
            return this._grid;
        }

        public string GetField(int position)
        {
            return _grid[position];
        }

        public void SetField(int position, string mark)
        {
            _grid[position] = mark;
        }

        public bool IsValidField(int position)
        {
            int numFields = (int)Math.Pow(_dimension, 2);
            if (position < 0 || numFields < position)
            {
                return false;
            }

            return true;
        }

        public bool IsEmptyField(int position)
        {
            return !this.GetField(position).Equals("x") && 
                !this.GetField(position).Equals("o");
        }
        
        public Board Duplicate()
        {
            string[] dupedGrid = (string[]) this._grid.Clone();
            return new Board(this._dimension, dupedGrid);
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
        
            foreach (string field in this._grid)
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
                return Equals(x._grid, y._grid) && x._dimension == y._dimension;
            }

            public int GetHashCode(Board obj)
            {
                return HashCode.Combine(obj._grid, obj._dimension);
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
            int dim = this._dimension;
            string[,] rows = new string[dim, dim];
            for (int i = 0; i < this._grid.Length; i++)
            {
                rows[i / dim, i % dim] = this._grid[i];
            }

            return rows;
        }
        
        private string[,] GetColumns()
        {
            int dim = this._dimension;
            string[,] cols = new string[dim, dim];
            for (int i = 0; i < this._grid.Length; i++)
            {
                cols[i % dim, i / dim] = this._grid[i];
            }

            return cols;
        }
        
        private string[,] GetDiagonals()
        {
            int dim = this._dimension;
            string[,] diags = new string[2, dim];
            for (int i = 0; i < this._grid.Length; i += (dim + 1))
            {
                diags[0, i % dim] = this._grid[i];
            }

            for (int i = dim - 1; i < this._grid.Length - 1; i += (dim - 1))
            {
                diags[1, i / dim] = this._grid[i];
            }

            return diags;
        }
    }
}