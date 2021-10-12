using System;
using src.GameLogic;

namespace src.UI
{
    public class Validator
    {
        public static bool IsMarkerValid(string marker)
        {
            marker = marker.ToLower();
            return marker == GlobalConstants.GlobalConstants.XMarker ||
                   marker == GlobalConstants.GlobalConstants.OMarker;
        }

        public static bool IsTurnOrderValid(string turnOrder)
        {
            return turnOrder == "1" || turnOrder == "2";
        }

        public static bool IsGameModeValid(string gameMode)
        {
            gameMode = gameMode.ToUpper();
            return gameMode == GlobalConstants.GlobalConstants.HumanVsComputer ||
                   gameMode == GlobalConstants.GlobalConstants.HumanVsHuman;
        }

        public static bool IsMoveValid(string move, string[] fields)
        {
            int index;
            bool isAnInteger = Int32.TryParse(move, out index);
            return isAnInteger && (IsMoveWithinBounds(index, fields.Length) && IsFieldVacant(index, fields));
        }

        private static bool IsFieldVacant(int index, string[] fields)
        {
            return BoardEvaluator.IsEmptyField(fields[index]);
        }
        private static bool IsMoveWithinBounds(int index, int gridLength)
        {
            return index < gridLength && 0 <= index;
        }
        
        public static bool StrategyLevel(string strategy)
        {
            strategy = strategy.ToUpper();
            return strategy == GlobalConstants.GlobalConstants.EasyStrategy || strategy == GlobalConstants.GlobalConstants.HardStrategy;
        }
        
        public static bool IsBoardDimensionValid(string dimension)
        {
            return dimension == "3" || dimension == "4";
        }
    }
}
