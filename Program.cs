using System;

namespace Encryption_Task8
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DataPharser dp = new DataPharser();
            EncryptionTools tools = new EncryptionTools();
            var message = dp.getMessage("Message.txt");
            var step1 = tools.Encrypt(message);
            Console.WriteLine(step1);
            var step2 = tools.Decrypt(step1);
            Console.WriteLine(step2);
            dp.watchPairs();
        }
    }
}