using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Utils
{
    public class CryptoUtil
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly static string cipherKey = ConfigurationManager.AppSettings["CipherKey"];

        public static byte[] GetSaltedPasswordHash(String plainText, String salt)
        {
            byte[] plainTextByte = Encoding.UTF8.GetBytes(plainText);
            byte[] saltByte = Encoding.UTF8.GetBytes(salt);
            byte[] plainTextWithSaltBytes =
              new byte[plainTextByte.Length + saltByte.Length];

            for (int i = 0; i < plainTextByte.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainTextByte[i];
            }
            for (int i = 0; i < saltByte.Length; i++)
            {
                plainTextWithSaltBytes[plainTextByte.Length + i] = saltByte[i];
            }

            return new SHA256Managed().ComputeHash(plainTextWithSaltBytes);
        }

        public static byte[] Encrypt(string plainText, string salt)
        {

            var aesAlg = NewRijndaelManaged(salt);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return msEncrypt.ToArray();
        }

        public static string EncryptToBase64(string plainText, string salt)
        {

            var aesAlg = NewRijndaelManaged(salt);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public static string Decrypt(byte[] cipherArray, string salt)
        {
            string plainText;

            var aesAlg = NewRijndaelManaged(salt);
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new MemoryStream(cipherArray))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        plainText = srDecrypt.ReadToEnd();
                    }
                }
            }
            return plainText;

        }

        public static string DecryptFromBase64(string cipherText, string salt)
        {
            string plainText;

            var aesAlg = NewRijndaelManaged(salt);
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        plainText = srDecrypt.ReadToEnd();
                    }
                }
            }
            return plainText;

        }

        private static RijndaelManaged NewRijndaelManaged(string salt)
        {
            if (salt == null) throw new ArgumentNullException("salt");
            var saltBytes = Encoding.ASCII.GetBytes(salt);
            var key = new Rfc2898DeriveBytes(Encoding.UTF8.GetString(Convert.FromBase64String(cipherKey)), saltBytes);

            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            return aesAlg;
        }
    }
}
