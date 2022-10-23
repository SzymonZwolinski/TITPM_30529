using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

class Program
{
        static IDictionary<char, int> returnWordCount(string word)
        {
            Dictionary<char, int> obj = new();
            word.ToList().ForEach(i =>
            {
                var found = obj.TryGetValue(i, out int value) ? obj[i]++ : obj[i] = 1;
            });
            Dictionary<char, int> sorted = obj.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
            return sorted;
        }
        static Dictionary<char, string> ans = new();
        static IDictionary<char, string> createTree(string word)
        {
            IDictionary<char, int> probClass = returnWordCount(word);
            probClass.Keys.ToList().ForEach(i => { ans[i] = ""; });
            if (probClass.Keys.Count > 1)
            {
                shannonFanno(charValueDic: probClass);
            }
            else ans[probClass.Keys.ToList()[0]] = "0";
            return ans;
        }

        static void shannonFanno(
            IDictionary<char, int> charValueDic = null,
            string codeBit = ""
           )
        {
            charValueDic.Keys.ToList().ForEach(i => ans[i] = codeBit);
            List<char> keys = charValueDic.Keys.ToList();
            List<int> val = charValueDic.Values.ToList();
            List<int> diff = new();
            if (val.Count > 1)
            {
                diff = val.Select((_, index) => Math.Abs(val.ToArray()[0..(index + 1)].Sum() - val.ToArray()[(index + 1)..].Sum())).ToList().GetRange(0, val.Count - 1);
                var min = diff.IndexOf(diff.Min());
                Dictionary<char, int> left = new();
                Dictionary<char, int> right = new();
                for (int i = 0; i < min + 1; i++)
                {
                    left[keys[i]] = val[i];
                }
                for (int i = min + 1; i < val.Count(); i++)
                {
                    right[keys[i]] = val[i];
                }
                if (left.Values.Count > 0)
                {
                    shannonFanno(charValueDic: left, codeBit: codeBit + "0");
                }
                if (right.Values.Count > 0)
                {
                    shannonFanno(charValueDic: right, codeBit: codeBit + "1");
                }
            }

        }

        static void Main()
        {
            Console.WriteLine("Wpisz slowo");
            string args = Console.ReadLine();
          
            var ans = JsonSerializer.Serialize(createTree(args));
            Console.WriteLine(ans);

        }
}
