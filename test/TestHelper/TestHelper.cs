using System;
using System.IO;

namespace test.TestHelper
{
    public class TestHelper
    {
        public static void SetInput(string input)
        {
            StringReader stringReader = new StringReader(input);
            Console.SetIn(stringReader);
        }
    }
}
