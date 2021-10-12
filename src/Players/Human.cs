using System;
using src.UI;


namespace src.Players
{
    public class Human : Player
    {
        public override int Move(string[] fields)
        {
            return Prompt.GetPlayerMove(GetName(), fields);
        }
    }
}