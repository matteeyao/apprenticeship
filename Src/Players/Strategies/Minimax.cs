using src.GameLogic;
using System;
using System.Collections.Generic;
using src.OppositeMarkers;

namespace src.Players.Strategies
{
    public class Minimax
    {
        public static int MinOrMaxScore(string[] fields, string marker, int depth, 
            bool maximizingPlayer, int originalDepth, int alpha = -1000, int beta = 1000)
        {
            if (Rules.IsOver(fields) || originalDepth - depth == 7)
            {
                int score = Score(fields) * depth;
                return maximizingPlayer ? score : score * -1;
            }

            maximizingPlayer = !maximizingPlayer;
            string oppositeMarker = OppositeMarker.Marker(marker);
            List<string[]> children = FindNextBoards(fields, oppositeMarker);

            if (maximizingPlayer)
            {
                int value;
                foreach (string[] child in children)
                {
                    if (beta <= alpha)
                    {
                        break;
                    }

                    value = MinOrMaxScore(child, oppositeMarker, depth - 1, true, originalDepth, alpha, beta);
                    alpha = Math.Max(alpha, value);
                }

                return alpha;
            }
            else
            {
                int value;
                foreach (string[] child in children)
                {
                    if (beta <= alpha)
                    {
                        break;
                    }

                    value = MinOrMaxScore(child, oppositeMarker, depth - 1, false, originalDepth, alpha, beta);
                    beta = Math.Min(beta, value);
                }

                return beta;
            }
        }

        public static List<string[]> FindNextBoards(string[] fields, string marker)
        {
            List<string[]> children = new List<string[]>();
            string[] availableFields = BoardEvaluator.FilterEmptyFields(fields);

            foreach (string availableField in availableFields)
            {
                string[] child = (string[]) fields.Clone();
                int index = Int32.Parse(availableField);
                child[index] = marker;
                children.Add(child);
            }

            return children;
        }

        private static int Score(string[] fields)
        {
            return Rules.IsWon(fields) ? 10 : 0;
        }
    }
}