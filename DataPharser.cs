using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Encryption_Task8
{
    internal class DataPharser
    {
        private char[] getPart(char[] mass, int size)
        {
            char[] result = new char[size];
            Array.Copy(mass, mass.Length - size, result, 0, size);
            return result;
        }

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
                char[] eng = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'V', 'X', 'Y', 'Z', 'W' };
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
            int keySize = key.Count;
            int alphabetSize = alphabet.Count;
            int size;
            if (alphabetSize > keySize)
            {
                size = keySize;
            }
            else
            {
                size = alphabetSize;
            }
            for (int i = 0; i < size; i++)
            {
                result.Add(key[i], alphabet[i]);
            }
            foreach (var item in getLostPairs(key, alphabet))
            {
                result.Add(item.Key, item.Value);
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
        private Dictionary<char, char> getLostPairs(List<char> firstList, List<char> secondList)
        {
            var dic = new Dictionary<char, char>();
            int firstListSize = firstList.Count;
            int secondListSize = secondList.Count;
            int difference = 0;
            bool isOdd = false;
            List<char> tmpList;
            if (firstListSize > secondListSize)
            {
                difference = firstListSize - secondListSize;
                if (difference % 2 != 0)
                {
                    isOdd = true;
                }
                tmpList = getPart(firstList.ToArray(), difference).ToList();
                if (tmpList.Count == 1)
                {
                    dic.Add(tmpList[0], '@');
                }
                else if (tmpList.Count == 0)
                {
                    Console.WriteLine("Error");
                }
                else if (isOdd)
                {
                    for (int i = 0; i < tmpList.Count - 2; i++)
                    {
                        dic.Add(tmpList[i], tmpList[i + 1]);
                    }
                    dic.Add(tmpList[tmpList.Count - 1], '#');
                }
                else
                {
                    for (int i = 0; i < tmpList.Count - 1; i++)
                    {
                        dic.Add(tmpList[i], tmpList[i + 1]);
                    }
                }
            }
            else if (secondListSize > firstListSize)
            {
                difference = secondListSize - firstListSize;
                if (difference % 2 != 0)
                {
                    isOdd = true;
                }
                tmpList = getPart(secondList.ToArray(), difference).ToList();
                if (tmpList.Count == 1)
                {
                    dic.Add(tmpList[0], '@');
                }
                else if (tmpList.Count == 0)
                {
                    Console.WriteLine("Error");
                }
                else if (isOdd)
                {
                    for (int i = 0; i < tmpList.Count - 2; i++)
                    {
                        dic.Add(tmpList[i], tmpList[i + 1]);
                    }
                    dic.Add(tmpList[tmpList.Count - 1], '#');
                }
                else
                {
                    for (int i = 0; i < tmpList.Count - 1; i++)
                    {
                        dic.Add(tmpList[i], tmpList[i + 1]);
                    }
                }
            }
            return dic;
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