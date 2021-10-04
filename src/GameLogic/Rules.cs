namespace src.GameLogic
{
    public class Rules
    {
        public static bool IsWon(string[] fields)
        {
            return BoardEvaluator.IsAnySequenceUniform(fields);
        }

        public static bool IsOver(string[] fields)
        {
            return IsWon(fields) || BoardEvaluator.IsBoardFilled(fields);
        }
    }
}