using System.Collections.Generic;
using ConsoleApp;
using NLipsum.Core;
using NUnit.Framework;

namespace Tests
{
    
    /// <summary>
    /// Basic tests for the Program.cs file
    /// </summary>
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

        [Test]
        public void MultipleWordsTest()
        {
            string text = "ok go stop now";

            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
            Assert.AreEqual(11, result.CharCount);
            Assert.AreEqual(4, result.WordCount);
        }

        [Test]
        public void LongestWordsTest()
        {
            string text = "ok go stop hello different tester programming";

            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
            Assert.AreEqual(7, result.WordCount);

            List<string> stringList = new List<string>
            {
                "programming",
                "different",
                "tester",
                "hello",
                "stop"
            };
            Assert.AreEqual(stringList, result.FiveLongestWords);
        }

        [Test]
        public void ShortestWordsTest()
        {
            string text = "ok go stop hello different tester programming";

            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
            Assert.AreEqual(7, result.WordCount);

            List<string> stringList = new List<string>
            {
                "ok",
                "go",
                "stop",
                "hello",
                "tester"
            };
            Assert.AreEqual(stringList, result.FiveShortestWords);
        }
        
        [Test]
        public void MostFrequentWordTest()
        {
            string text = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered many many many";

            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
            Assert.AreEqual("many", result.TenFrequentWords[0]);
        }
        
        [Test]
        public void MostFrequentWordsTest()
        {
            string text = "There are of many variations of variations of many many many";

            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
            
            List<string> frequentWords = new List<string>
            {
                "many",
                "of",
                "variations",
                "There",
                "are"
            };
            
            Assert.AreEqual(frequentWords, result.TenFrequentWords);
        }
        
        [Test]
        public void CharCountTest()
        {
            string text = " asdf  asf sefa sfeeek  oj";

            IpsumStreamResult result = BoosterApp.ReadStream(text.Length, 1, text);
            Assert.AreEqual(text, result.FinalString);
            Assert.AreEqual(3, result.CharCountTotal['a']);
        }
    }
}