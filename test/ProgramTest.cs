using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace test
{
    public class ProgramTest
    {
        Program program;

        [SetUp]
        public void StartANewGame()
        {
            program = new Program();
        }
    }
}