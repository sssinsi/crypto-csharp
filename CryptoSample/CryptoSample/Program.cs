using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //AES
            Console.WriteLine("===========");
            Console.WriteLine("AES Sampple");
            Console.WriteLine("===========");
            
            Console.WriteLine("文字を入力してください：");
            string text = Console.ReadLine();

            AesCrypto a = new AesCrypto();
            var aesCrypt = a.Encrypt(text);

            Console.WriteLine("暗号化文字：");
            Console.WriteLine(aesCrypt);

            var aesDecrypt = a.Decrypt(aesCrypt);
            Console.WriteLine("復号化文字：");
            Console.WriteLine(aesDecrypt);

            
            //RSA
            Console.WriteLine("===========");
            Console.WriteLine("RSA Sampple");
            Console.WriteLine("===========");

            string publicKey=string.Empty;
            string privateKey=string.Empty;
            RsaCrypto.CreateKeys(out publicKey,out  privateKey);

            string str = Console.ReadLine();
            string rsaCrypt = RsaCrypto.Encrypt(str, publicKey);
            Console.WriteLine(rsaCrypt);

            if (string.IsNullOrEmpty(rsaCrypt))
            {
                Console.ReadLine();
                return;
            }

            string rsaDecrypt = RsaCrypto.Decrypt(rsaCrypt, privateKey);
            Console.WriteLine(rsaDecrypt);
            Console.ReadLine();
        }
    }
}
