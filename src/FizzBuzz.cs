using System;
using System.Linq;

namespace src
{
    public class FizzBuzz
    {
        public static string[] GetFizzBuzzArray(int n)
        {
            if (n <= 0) throw new ArgumentOutOfRangeException();

            return Enumerable.Range(1, n).Select(x =>
                x % 15 == 0 ? "FizzBuzz" :
                x % 3 == 0 ? "Fizz" :
                x % 5 == 0 ? "Buzz" :
                x.ToString()).ToArray();
        }
    }
}