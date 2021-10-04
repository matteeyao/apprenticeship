using src.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using src.OppositeMarkers;
using src.UI;

namespace src.Setup
{
    public class Setup
    {
        public Player[] players { get; private set; }
        private int boardDimension;

        public Setup(Player[] players)
        {
            this.players = players;
        }

        public int GetBoardDimension()
        {
            return boardDimension;
        }

        public void SetBoardDimension(string boardDimension)
        {
            this.boardDimension = Int32.Parse(boardDimension);
        }

        public void Start(string gameMode)
        {
            SetPlayerName(players.First(), Prompt.GetPlayerName());
            string marker = Prompt.GetInput(MessageHandler.InquireForMarker, Validator.IsMarkerValid);
            SetPlayerMarker(players.First(), marker);
            SetSecondPlayerName();
            SetPlayerMarker(players.Last(), OppositeMarker.Marker(PlayerMarker(players.First())));
            AssignTurnOrder(Prompt.GetTurnOrder(PlayerName(players.First())));
            string boardDimensions = Prompt.GetInput(MessageHandler.AskPlayerForBoardDimmensions,
                Validator.IsBoardDimensionValid);
            SetBoardDimension(boardDimensions);
        }

        private void SetSecondPlayerName()
        {
            bool twoPlayerGame = players.Last() is Human;

            if (twoPlayerGame)
            {
                SetPlayerName(players.Last(), Prompt.GetPlayerName());
            }
            else
            {
                players.Last().SetName("Johnny 5");
            }
        }

        private void SetPlayerName(Player player, string name)
        {
            player.SetName(name);
        }

        private void SetPlayerMarker(Player player, string marker)
        {
            player.SetMarker(marker);
        }

        private void AssignTurnOrder(string turnOrder)
        {
            if (turnOrder == "2")
            {
                players = new Player[] {players.Last(), players.First()};
            }
        }

        private string PlayerMarker(Player player)
        {
            return player.GetMarker();
        }

        private string PlayerName(Player player)
        {
            return player.GetName();
        }
    }
}