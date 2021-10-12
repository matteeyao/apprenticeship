using src.Players;
using System;
using System.Linq;
using src.GameLogic;
using src.Players.Strategies;
using src.UI;

namespace src
{
    class Program
    {
        private Board board;
        private Setup.Setup setup;
        private Player[] players;

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        public void Start()
        {
            string gameMode = Prompt.GetInput(MessageHandler.AskPlayerForGameMode, Validator.IsGameModeValid);
            ReadGameModeAndInitializePlayers(gameMode);
            Setup(gameMode);
            Player lastPlayerToMove = Moves();
            WonOrTiedMessage(lastPlayerToMove);
        }

        private void Setup(string gameMode)
        {
            setup = new Setup.Setup(players);
            setup.Start(gameMode);
            players = setup.players;
            board = new Board(setup.GetBoardDimension());
        }

        private void ReadGameModeAndInitializePlayers(string gameMode)
        {
            if (GlobalConstants.GlobalConstants.HumanVsHuman == gameMode)
            {
                InitializeHumanPlayers();
            }
            else
            {
                InitializeHumanVsComputerPlayers();
            }
        }

        private Player Moves()
        {
            Player currentPlayer = FirstPlayer();
            Turn(currentPlayer);
            while (!Rules.IsOver(board.GetGrid()))
            {
                currentPlayer = currentPlayer == FirstPlayer() ? SecondPlayer() : FirstPlayer();
                Turn(currentPlayer);
            }
            
            MessageHandler.PrintBoard(board.GetGrid());
            return currentPlayer;
        }

        private void WonOrTiedMessage(Player lastPlayerToMove)
        {
            if (Rules.IsWon(board.GetGrid()))
            {
                MessageHandler.Winner(lastPlayerToMove.GetName());
            }
            else
            {
                MessageHandler.Tied();
            }
        }

        private void Turn(Player currentPlayer)
        {
            MessageHandler.PrintBoard(board.GetGrid());
            int move = currentPlayer.Move(board.GetGrid());
            MarkBoard(move, currentPlayer.GetMarker());
        }

        private void InitializeHumanPlayers()
        {
            players = new Player[] {new Human(), new Human()};
        }

        private void InitializeHumanVsComputerPlayers()
        {
            string strategyLevel = Prompt.GetInput(MessageHandler.AskPlayerForDifficultyLevel, Validator.StrategyLevel);

            if (strategyLevel == GlobalConstants.GlobalConstants.EasyStrategy)
            {
                players = new Player[] { new Human(), new Computer(new EasyStrategy()) };
            }
        }

        private Player FirstPlayer()
        {
            return players.First();
        }

        private Player SecondPlayer()
        {
            return players.Last();
        }

        private void MarkBoard(int space, string marker)
        {
            board.SetField(space, marker);
        }
    }
}