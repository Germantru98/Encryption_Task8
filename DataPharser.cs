using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Encryption_Task8
{
    internal class DataPharser
    {
        public List<char> getKey()
        {
            using (StreamReader stream = new StreamReader("Key.txt", Encoding.Default))
            {
                return stream.ReadToEnd().ToUpper().ToList();
            }
        }

        public string getMessage(string filePath)
        {
            using (StreamReader stream = new StreamReader(filePath, Encoding.Default))
            {
                var tmp = stream.ReadToEnd();
                char[] eng = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'V', 'X', 'Y', 'Z' };
                List<string> message = tmp.Split().ToList();
                string result = "";
                foreach (var item in message)
                {
                    if (!string.IsNullOrEmpty(item) && !eng.Contains(char.ToUpper(item[0])))
                    {
                        result += item + " ";
                    }
                }
                return result.ToUpper().Trim(' ');
            }
        }

        public Dictionary<char, char> GetPairs()
        {
            var result = new Dictionary<char, char>();
            var key = getKey().Distinct().ToList();
            var alphabet = GetAlphabet().Except(key).ToList();
            for (int i = 0; i < key.Count; i++)
            {
                result.Add(key[i], alphabet[i]);
            }
            return result;
        }

        public List<char> GetAlphabet()
        {
            using (StreamReader stream = new StreamReader("Alphabet.txt", Encoding.Default))
            {
                return stream.ReadToEnd().ToList();
            }
        }

        public KeyValuePair<char, char> getPairChar(char c)
        {
            var pairs = GetPairs();
            KeyValuePair<char, char> result = new KeyValuePair<char, char>('$', '$');
            foreach (var item in pairs.Keys)
            {
                if (item == c)
                {
                    result = new KeyValuePair<char, char>(c, pairs[c]);
                }
            }
            if (result.Key == '$')
            {
                foreach (var item in pairs)
                {
                    if (item.Value == c)
                    {
                        result = new KeyValuePair<char, char>(item.Key, item.Value);
                    }
                }
            }
            return result;
        }

        public void watchPairs()
        {
            foreach (var item in GetPairs())
            {
                Console.WriteLine(item);
            }
        }
    }
}