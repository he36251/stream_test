using System;
using System.IO;
using System.Text;
using DevTest;
using NLipsum.Core;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly:InternalsVisibleTo("Testing")]
namespace ConsoleApp
{
    class BoosterApp
    {
        static void Main(string[] args)
        {
            ReadStream(100, 1);
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
            //Check if a number argument is present. Use default value if not.
           
            // if (args.Length == 0)
            // {
            //     Console.WriteLine($"Please enter a number. Using default value: {maxLength}");
            // }
            // else
            // {
            //     bool numberParser = Int32.TryParse(args[0], out maxLength);
            //     if (!numberParser)
            //     {
            //         int defaultLength = 100;
            //
            //         Console.WriteLine($"Please enter a number. Using default value: {defaultLength}");
            //         maxLength = defaultLength;
            //     }
            // }


            string text = LipsumGenerator.Generate(1);

            // Console.WriteLine(text);

            int overallCount = 0;
            int charCount = 0;

            string completedString = "";

            LorumIpsumStream stream = null;
            
            if (String.IsNullOrWhiteSpace(customString))
            {
                stream = new LorumIpsumStream(allocatedSize);
            }
            else
            {
                stream = new CustomStream(customString);
            }


            using (stream)
            {
                //Since we are checking per char, use 1 byte
                byte[] buffer = new byte[1];

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, 1);
                    if (bytesRead == 0)
                        break;

                    char charValue = Encoding.UTF8.GetString(buffer).ToCharArray()[0];

                    //Check if it's a readable character
                    if (!Char.IsControl(charValue))
                    {
                        charCount++;
                    }

                    overallCount++;
                    if (overallCount >= maxLength)
                    {
                        break;
                    }

                    completedString += charValue;

                    if (debug)
                    {
                        //Stats
                        Console.Clear();
                        Console.WriteLine(completedString);
                        Console.WriteLine($"Chars: {charCount}");
                    
                        //Block for readability/testing purposes 
                        Thread.Sleep(50 );
                    }
                }
            }

            return new IpsumStreamResult
            {
                FinalString = completedString,
                CharCount = charCount
            };
        }
    }
}