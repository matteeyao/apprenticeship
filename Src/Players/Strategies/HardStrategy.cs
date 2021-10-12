using System;
using System.Collections.Generic;
using System.Linq;
using src.GameLogic;

namespace src.Players.Strategies
{
    public class HardStrategy : IComputerStrategy
    {
        public int BestMove(string[] fields, string marker)
        {
            Dictionary<int, int> scoresByMove = ScoresByMove(fields, marker);
            /* Applies an accumulator function over a sequence. The specified seed value
             is used as the initial accumulator value, and the specified function is used
             to select the result value. */
            KeyValuePair<int, int> highestScoreByMove = scoresByMove.Aggregate(
                (left, right) => left.Value > right.Value ? left : right);
            return highestScoreByMove.Key;
        }

        private Dictionary<int, int> ScoresByMove(string[] fields, string marker)
        {
            Dictionary<int, int> ScoresByMove = new Dictionary<int, int>();
            string[] availableFields = BoardEvaluator.FilterEmptyFields(fields);
            List<string[]> nextBoards = Minimax.FindNextBoards(fields, marker);
            int index = 0;

            foreach (string availableField in availableFields)
            {
                int depth = availableFields.Count();
                int move = Int32.Parse(availableField);
                int score = Minimax.MinOrMaxScore(nextBoards[index], marker, depth, true, depth, -1000, 1000);
                ScoresByMove.Add(move, score);
                index++;
            }

            return ScoresByMove;
        }
    }
}