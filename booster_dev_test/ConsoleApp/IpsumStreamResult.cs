using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    public class IpsumStreamResult
    {
        public string FinalString { get; set; }
        public int CharCount { get; set; }
        public int WordCount { get; set; }
        public string LongestWord { get; set; }
        public List<string> Words { get; set; }
        
        public List<string> FiveLongestWords => Words.OrderByDescending(x => x.Length).Take(5).ToList();
        public List<string> FiveShortestWords => Words.OrderBy(x => x.Length).Take(5).ToList();

        public List<string> TenFrequentWords
        {
            get
            {
                Dictionary<string, int> wordDictionary = new Dictionary<string, int>();
                
                foreach (string word in Words)
                {
                    if (!wordDictionary.TryAdd(word, 1))
                    {
                        wordDictionary[word] += 1;
                    }
                }
                
                return wordDictionary.OrderByDescending(x => x.Value).Select(x => x.Key).Take(10).ToList();
            }
        }

        /// <summary>
        /// Helper method
        /// </summary>
        private string FlattenWords
        {
            get
            {
                //Use StringBuilder to save memory - handles only a single string object in runtime
                StringBuilder sb = new StringBuilder();
                foreach (string word in Words)
                {
                    sb.Append(word);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Ordered dictionary of chars descending by their occurence
        /// </summary>
        public Dictionary<char, int> CharCountTotal
        {
            get
            {
                char[] chars = FlattenWords.ToCharArray();
                
                //Count occurence of each character
                Dictionary<char, int> charDictionary = new Dictionary<char, int>();
                foreach (char c in chars)
                {
                    if (!charDictionary.TryAdd(c, 1))
                    {
                        charDictionary[c] += 1;
                    }
                }

                return charDictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            }
        } 
        
    }
}