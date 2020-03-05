using ConsoleApp;
using NLipsum.Core;
using NUnit.Framework;

namespace Tests
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
            Assert.AreEqual(0, result.CharCount);
            Assert.AreEqual(0, result.WordCount);
        }
        
        [Test]
        public void WhitespaceTest()
        {
            string text = " ";
            
            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
            Assert.AreEqual(0, result.CharCount);
            Assert.AreEqual(0, result.WordCount);
        }
        
        [Test]
        public void SingleCharTest()
        {
            string text = "y";
            
            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
            Assert.AreEqual(1, result.CharCount);
            Assert.AreEqual(1, result.WordCount);
        }
        
        [Test]
        public void SingleWordTest()
        {
            string text = "single";
            
            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
            Assert.AreEqual(6, result.CharCount);
            Assert.AreEqual(1, result.WordCount);
        }
    }
}