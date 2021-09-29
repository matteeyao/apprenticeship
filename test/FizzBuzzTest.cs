using NUnit.Framework;
using src;
using System;

namespace test
{
    [TestFixture]
    public class FizzBuzzTest
    {
        [Test]
        public void FizzBuzzTest_1()
        {
            int input = 1;
            string[] expected = { "1" };

            string[] actual = FizzBuzz.GetFizzBuzzArray(input);
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FizzBuzzTest_3()
        {
            int input = 3;
            string[] expected = { "1", "2", "Fizz" };

            string[] actual = FizzBuzz.GetFizzBuzzArray(input);
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FizzBuzzTest_5()
        {
            int input = 5;
            string[] expected = { "1", "2", "Fizz", "4", "Buzz" };

            string[] actual = FizzBuzz.GetFizzBuzzArray(input);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void FizzBuzzTest_15()
        {
            int input = 15;
            string[] expected = { "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz" };

            string[] actual = FizzBuzz.GetFizzBuzzArray(input);

            Assert.AreEqual(expected, actual);
        }
    }
}