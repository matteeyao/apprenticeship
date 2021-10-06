using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] rows =
            {
                {"1", "2", "3"}, 
                {"4", "5", "6"},
                {"7", "8", "9"}
            };

            PrintBoard(rows, 3);

            // Console.WriteLine(String.Join(" ", rows.Cast<string>()));

            /* At most pass newly instaniated classes to new classes, no processing messages
             Tests: Does it instantiate all of the following classes that it needs*/
        }

        private static string[] PrintBoard(string[,] rows, Dictionary<string, Player> players)
        {
            string board = "";
            for (int rowIdx = 0; rowIdx < rows.Length; rowIdx++)
            {
               
            }
            // return board;
        }

        private static string FormatSignature(string proxy, Dictionary<string, Player> players)
        {
            return players.ContainsKey(proxy) ? players[proxy].mark :
                proxy.Length < 2 ? $" {proxy}" : proxy;
        }

        private static string PrintRow(string[] row)
        {
            string firstColProxy = rows[rowIdx, 0];
            string firstColSignature = FormatSignature(firstColProxy, playerOneMark, playerTwoMark);
            string row = $"{firstColSignature} |";
                
            for (int colIdx = 1; colIdx < rows.Length - 1; colIdx++)
            {
                string proxy = rows[rowIdx, colIdx];
                string placeholder = FormatSignature(firstColProxy, playerOneMark, playerTwoMark);
                row += $" {placeholder} |";
            }
                
            string lastColProxy = rows[rowIdx, rows.Length - 1];
            string lastColPlaceholder = FormatSignature(lastColProxy, playerOneMark, playerTwoMark);
            row = string.Format(@"{0} | {1}", row, lastColPlaceholder);
            board = string.Format(@"{0}\n{1}\n{2}\n", board, PrintRowSeparator(rows.Length), row);
        }
        
        private static string PrintRowBottomBorder(int dimension)
        {
            int times = (dimension - 2) * 4 + 2 * 3 + (dimension - 1);
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < times; i++)
            {
                sb.Append("-");
            }
            return sb.ToString();
        }
    }
}