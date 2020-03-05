using ConsoleApp;
using NUnit.Framework;

namespace Testing
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            BoosterApp.ReadStream(100, 1);
            Assert.Pass();
        }
    }
}