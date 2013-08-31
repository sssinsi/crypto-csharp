using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CryptoSample
{
    /// <summary>
    /// 公開鍵暗号方式(RSA)
    /// </summary>
    public static class RsaCrypto
    {
        public static void CreateKeys(out string publicKey, out string privateKey) {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //公開鍵をXML形式で取得
            publicKey = rsa.ToXmlString(false);
            //秘密鍵をXML形式で取得
            privateKey = rsa.ToXmlString(true);
        }
        /// <summary>
        /// 公開鍵を使って文字列を暗号化する
        /// </summary>
        /// <param name="str">暗号化する文字列</param>
        /// <param name="publicKey">暗号化に使用する公開鍵（XML形式）</param>
        /// <returns>暗号化された文字列</returns>
        public static string Encrypt(string str, string publicKey) {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            //公開鍵を指定
            if (!CanCryptText(rsa.KeySize, str))
            {
                Console.WriteLine("文字列が大きすぎます。key size:{0}", rsa.KeySize);
                return string.Empty;
            }
            rsa.FromXmlString(publicKey);

            //暗号化する文字列をバイト列に設定
            byte[] data = Encoding.UTF8.GetBytes(str);
            //暗号化する
            byte[] encryptedData = rsa.Encrypt(data, false);

            //Base64で結果を文字列に変換
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 秘密鍵を使って文字列を復号化する
        /// </summary>
        /// <param name="str">Encryptメソッドにより暗号化された文字列</param>
        /// <param name="privateKey">復号化に必要な秘密鍵（XML形式）</param>
        /// <returns>復号化された文字列</returns>
        public static string Decrypt(string str, string privateKey) {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //秘密鍵を指定
            rsa.FromXmlString(privateKey);

            //復号化する文字列をバイト配列にする
            byte[] data = Convert.FromBase64String(str);
            //復号化する
            byte[] decryptedData = rsa.Decrypt(data, false);

            //結果を文字列に変換
            return Encoding.UTF8.GetString(decryptedData);
        }

        private static bool CanCryptText(int p, string str)
        {
            Encoding unicodeEnc = Encoding.GetEncoding("Shift-jis");
            int count = unicodeEnc.GetByteCount(str);
            Console.WriteLine("文字サイズ:{0}",count);
            //(PKCS#1 v1.5) の場合
            return (p/8)-11 >= count;
        }
    }

  
}
