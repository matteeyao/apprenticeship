using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Players.Strategies;
using src.UI;

namespace src.Players
{
    public class Computer : Player
    {
        private IComputerStrategy strategy;

        public Computer(IComputerStrategy strategy)
        {
            this.strategy = strategy;
        }

        public override int Move(string[] fields)
        {
            MessageHandler.InquireForMove(GetName());
            return strategy.BestMove(fields, GetMarker());
        }
    }
}