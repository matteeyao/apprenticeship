using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.UI
{
    public class Prompt
    {
        public static string GetPlayerName()
        {
            MessageHandler.InquireForName();
            return MessageHandler.ReadInput();
        }

        public static int GetPlayerMove(string name, string[] fields)
        {
            MessageHandler.InquireForMove(name);
            string move = MessageHandler.ReadInput();

            if (!Validator.IsMoveValid(move, fields))
            {
                MessageHandler.Invalid(move);
                return GetPlayerMove(name, fields);
            }
            else
            {
                return ConvertMoveToIndex(move);
            }
        }

        public static string GetTurnOrder(string name)
        {
            MessageHandler.InquireForTurnOrder(name);
            string turnOrder = MessageHandler.ReadInput();
            if (!Validator.IsTurnOrderValid(turnOrder))
            {
                MessageHandler.Invalid(turnOrder);
                return GetTurnOrder(name);
            }
            else
            {
                return turnOrder;
            }
        }

        public static string GetInput(Action message, Func<string, bool> validator)
        {
            message();
            string input = MessageHandler.ReadInput();
            if (!validator(input))
            {
                MessageHandler.Invalid(input);
                return GetInput(message, validator);
            }
            else
            {
                return input.ToLower();
            }
        }

        private static int ConvertMoveToIndex(string move)
        {
            return Int32.Parse(move);
        }
    }
}