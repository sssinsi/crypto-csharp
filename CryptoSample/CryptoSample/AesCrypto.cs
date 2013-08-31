using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CryptoSample
{
    /// <summary>
    /// 共通鍵暗号方式(AES)
    /// </summary>
    public class AesCrypto
    {
        /// <summary>
        /// 128bit(16byte)INITIAL VECTOR
        /// </summary>
        private readonly string AES_IV = "A8TS!FD#AD5RDIHF";
        /// <summary>
        /// 128bit(16byte)Key
        /// </summary>
        private readonly string AES_KEY = "9&KD(W4DKT>FD7O@";

        /// <summary>
        /// 文字列をAESで暗号化
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Encrypt(string text)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AES_IV);
            aes.Key = Encoding.UTF8.GetBytes(AES_KEY);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            //文字列をバイト型配列に変換
            byte[] src = Encoding.Unicode.GetBytes(text);

            //暗号化する
            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                //バイト型配列からBase64形式の文字列に変換
                return Convert.ToBase64String(dest);
            }

        }

        public string Decrypt(string text)
        {
            //AES暗号化サービスプロバイダ
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AES_IV);
            aes.Key = Encoding.UTF8.GetBytes(AES_KEY);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            //Base64形式の文字列からバイト型配列に変換
            byte[] src = Convert.FromBase64String(text);

            //複合する
            using (ICryptoTransform decrypt = aes.CreateDecryptor()) {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.Unicode.GetString(dest);
            }
        }

    }
}
