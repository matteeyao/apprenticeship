using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.GameLogic;

namespace src.Players.Strategies
{
    public class EasyStrategy : IComputerStrategy
    {
        public int BestMove(string[] fields, string marker)
        {
            string[][] sets = BoardEvaluator.GenerateRowColumnAndDiagonalSequences(fields);
            string bestMove;

            if (IsWinnable(sets, marker))
            {
                bestMove = FindWinningMove(FindWinningSet(sets, marker));
            }
            else
            {
                bestMove = RandomMove(fields);
            }

            return Int32.Parse(bestMove);
        }
        
        public string RandomMove(string[] fields)
        {
            Random rnd = new Random();
            string[] availableFields = BoardEvaluator.FilterEmptyFields(fields);
            int randomField = rnd.Next(availableFields.Count());
            return availableFields[randomField];
        }

        public bool IsWinnable(string[][] sets, string marker)
        {
            return sets.Any(set => PossibleWinningSet(set, marker));
        }
        
        private string[] FindWinningSet(string[][] sets, string marker)
        {
            return sets.Where(set => PossibleWinningSet(set, marker)).First();
        }

        private bool PossibleWinningSet(string[] set, string marker)
        {
            return AllSpacesSameMarkerExceptOneSpace(set, marker) && FilterSetForEmptySpaces(set).Count() == 1;
        }
        
        private string FindWinningMove(string[] winningSet)
        {
            return FilterSetForEmptySpaces(winningSet).First();
        }
        
        private string[] FilterSetForMarker(string[] set, string marker)
        {
            return set.Where(space => space == marker).ToArray();
        }
        
        private bool AllSpacesSameMarkerExceptOneSpace(string[] set, string marker)
        {
            int lengthOfRow = set.Length;
            int numberOfMarkersRequired = lengthOfRow - 1;
            return FilterSetForMarker(set, marker).Count() == numberOfMarkersRequired;
        }

        private string[] FilterSetForEmptySpaces(string[] set)
        {
            return set.Where(field => BoardEvaluator.IsEmptyField(field)).ToArray();
        }
    }
}