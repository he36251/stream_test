using System;
using System.Collections.Generic;
using System.Text;
using DevTest;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly:InternalsVisibleTo("Tests")]
namespace ConsoleApp
{
    class BoosterApp
    {
        //Run with default test settings
        static void Main(string[] args)
        {
            Console.WriteLine("Enter max character length and hit enter");
            string inputLength = "";

            ConsoleKeyInfo readKey;
            do
            {
                readKey = Console.ReadKey();
                inputLength += readKey.KeyChar;
            } while (readKey.Key != ConsoleKey.Enter);
            
            if (!int.TryParse(inputLength.Trim(), out int charLength))
            {
                throw new Exception("Please enter a number");
            }

            //Adjust any values to changes the length of the output text
            ReadStream(charLength, 1, null, false);
        }

        /// <summary>
        /// This outputs out a IpsumStreamResult object that has the final stats - eg, char count, word count.
        /// I've also added a debug flag so you can see stats in realtime in the console.
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="allocatedSize"></param>
        /// <param name="debug"></param>
        /// <param name="customString"></param>
        /// <returns></returns>
        public static IpsumStreamResult ReadStream(int maxLength, int allocatedSize, string customString = null, bool debug = false)
        {
            int overallCount = 0;
            int charCount = 0;
            int wordCount = 0;
            bool isWord = false;

            string longestWord = "";
            string currentWord = "";
            List<string> words = new List<string>();
            
            string completedString = "";

            //Tests uses custom strings, whereas running this app normally uses the LorumIpsumStream generator
            LorumIpsumStream stream = customString == null ? new LorumIpsumStream(allocatedSize) : new CustomStream(allocatedSize, customString);
            
            using (stream)
            {
                byte[] buffer = new byte[2];

                while (true)
                {
                    if (maxLength <= overallCount)
                    {
                        break;
                    }
                    
                    int bytesRead = stream.Read(buffer, 0, 2);
                    if (bytesRead == 0)
                        break;
                    
                    char charValue = Encoding.ASCII.GetString(buffer).ToCharArray()[0];

                    //Check if it's only characters - Lorum ipsum shouldn't have anything numbers/punctuations
                    if (Char.IsLetter(charValue))
                    {
                        charCount++;
                        currentWord += charValue;
                        if (!isWord)
                        {
                            isWord = true;
                        }
                    }
                    else
                    {
                        if (isWord)
                        {
                            wordCount++;
                            isWord = false;

                            if (longestWord.Length < currentWord.Length)
                            {
                                longestWord = currentWord;
                            }

                            words.Add(currentWord);
                            currentWord = "";
                        }
                    }

                    overallCount++;
                    completedString += charValue;

                    if (debug)
                    {
                        //Realtime stats
                        Console.Clear();
                        Console.WriteLine(completedString);
                        Console.WriteLine($"Char count: {charCount}");
                        Console.WriteLine($"Word count: {charCount}");
                        Console.WriteLine($"Longest word: {longestWord}");
                    
                        //Block for readability/testing purposes 
                        Thread.Sleep(1);
                    }
                }

                //Is last letter a part of a word
                if (isWord)
                {
                    wordCount++;
                    words.Add(currentWord);
                }
            }

            IpsumStreamResult result = new IpsumStreamResult
            {
                FinalString = completedString,
                CharCount = charCount,
                WordCount = wordCount,
                LongestWord = longestWord,
                
                Words = words
            };
            
            Console.WriteLine();
            Console.WriteLine("Done!");
            Console.WriteLine(result.FinalString);
            Console.WriteLine($"Char count: {result.CharCount}");
            Console.WriteLine($"Word count: {result.WordCount}");
            Console.WriteLine($"Longest word: {result.LongestWord}");
            
            Console.WriteLine($"Five longest words: {String.Join(" ", result.FiveLongestWords)}");
            Console.WriteLine($"Five shortest words: {String.Join(" ", result.FiveShortestWords)}");

            Console.WriteLine($"Ten most frequent words: {String.Join(" ", result.TenFrequentWords)}");
            
            foreach (KeyValuePair<char, int> pair in result.CharCountTotal)
            {
                Console.WriteLine($"Char: {pair.Key}, Occurence: {pair.Value}");
            }

            return result;
        }
    }
}