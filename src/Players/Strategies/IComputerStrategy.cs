namespace src.Players.Strategies
{
    public interface IComputerStrategy
    {
        int BestMove(string[] fields, string marker);
    }
}