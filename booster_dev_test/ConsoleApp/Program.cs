using System;
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
            ReadStream(100, 1, null, true);
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
            
            string completedString = "";

            //Tests uses custom strings, whereas running this app normally uses the LorumIpsumStream generator
            var stream = customString == null ? new LorumIpsumStream(allocatedSize) : new CustomStream(allocatedSize, customString);
            
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

                    //Check if it's a readable character
                    if (!Char.IsWhiteSpace(charValue) && charValue != '\0')
                    {
                        charCount++;
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
                        }
                    }

                    overallCount++;
                    completedString += charValue;

                    if (debug)
                    {
                        //Stats
                        Console.Clear();
                        Console.WriteLine(completedString);
                        Console.WriteLine($"Char count: {charCount}");
                        Console.WriteLine($"Word count: {charCount}");
                    
                        //Block for readability/testing purposes 
                        Thread.Sleep(50 );
                    }
                }

                if (isWord)
                {
                    wordCount++;
                }
            }

            return new IpsumStreamResult
            {
                FinalString = completedString,
                CharCount = charCount,
                WordCount = wordCount
            };
        }
    }
}