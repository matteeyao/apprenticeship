using NUnit.Framework;
using System;
using System.IO;
using src.Players;
using src.Players.Strategies;

namespace test.Players
{
    [TestFixture]
    public class ComputerTest
    {
        [Test]
        public void CanMakeAMove()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            IComputerStrategy hardStrategy = new HardStrategy();
            Computer computer = new Computer(hardStrategy);
            computer.SetMarker("x");
            string[] spaces = { "x", "x", "2", "3", "4", "5", "6", "7", "8" };
            Assert.AreEqual(2, computer.Move(spaces));
        }
    }
}