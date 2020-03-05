using System.Collections.Generic;
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
        public List<string> FiveLongestWords => Words.OrderByDescending(x => x.Length).Take(5).ToList();
        public List<string> FiveShortestWords => Words.OrderBy(x => x.Length).Take(5).ToList();
        
        public List<string> Words { get; set; }

        private string FlattenWords
        {
            get
            {
                //Use StringBuilder to save memory - handles only a single string object in runtime
                var sb = new StringBuilder();
                foreach (string word in Words)
                {
                    sb.Append(word);
                }

                return sb.ToString();
            }
        }

        public Dictionary<char, int> CharCountTotal
        {
            get
            {
                char[] chars = FlattenWords.ToCharArray();
                
                //Count occurence of each character
                var charDictionary = new Dictionary<char, int>();
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