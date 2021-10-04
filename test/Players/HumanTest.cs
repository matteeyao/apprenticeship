using NUnit.Framework;
using src.Players;
using System;

namespace test.Players
{
    [TestFixture]
    public class HumanTest
    {
        [Test]
        public void CanAssignAndRetrievePlayersName()
        {
            Human human = new Human();
            human.SetName("Bob");
            Assert.AreEqual("Bob", human.GetName());
        }

        [Test]
        public void CanAssignAndRetrievePlayersMarker()
        {
            Human human = new Human();
            human.SetMarker("x");
            Assert.AreEqual("x", human.GetMarker());
        }

        [Test]
        public void IsAbleToMakeAMove()
        {
            Human human = new Human();
            string[] spaces = {"0", "1", "2", "3", "4", "5", "6", "7", "8"};
            TestHelper.TestHelper.SetInput("4");
            Assert.AreEqual(4, human.Move(spaces));
        }
    }
}
