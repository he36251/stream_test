using ConsoleApp;
using NLipsum.Core;
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
        public void StandardLipsumTest()
        {
            string text = LipsumGenerator.Generate(1);
            
            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
        }

        [Test]
        public void EmptyStringTest()
        {
            string text = "";
            
            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
        }
        
        [Test]
        public void WhitespaceTest()
        {
            string text = " ";
            
            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
        }
    }
}