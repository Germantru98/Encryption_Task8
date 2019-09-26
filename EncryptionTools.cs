namespace Encryption_Task8
{
    internal class EncryptionTools
    {
        private DataPharser dp = new DataPharser();

        public string Encrypt(string message)
        {
            var pairs = dp.GetPairs();
            var result = "";
            char[] tmp = message.ToCharArray();
            for (int i = 0; i < message.Length; i++)
            {
                if (pairs.ContainsValue(message[i]) || pairs.ContainsKey(message[i]))
                {
                    var tmpPair = dp.getPairChar(message[i]);
                    if (message[i] == tmpPair.Key)
                    {
                        result += tmpPair.Value;
                    }
                    else
                    {
                        result += tmpPair.Key;
                    }
                }
                else
                {
                    result += message[i];
                }
            }
            return result;
        }

        public string Decrypt(string message)
        {
            return Encrypt(message);
        }
    }
}