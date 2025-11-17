using System;
using System.Security.Cryptography;
using System.Text;

namespace Games.Scripts.Utils.DataHelper
{
    public static class EncryptUtils
    {
        public static byte[] key = Convert.FromBase64String("AQIDBAUGBWGJCGSMDQ0PAA==");

        public static string Encrypt(string decodedStr)
        {
            // Common.Log("Encrypt: " + decodedStr);
            var data = Encoding.UTF8.GetBytes(decodedStr);
            using (var csp = new AesCryptoServiceProvider())
            {
                csp.KeySize = 256;
                csp.BlockSize = 128;
                csp.Key = key;
                csp.Padding = PaddingMode.PKCS7;
                csp.Mode = CipherMode.ECB;

                using (var encrypter = csp.CreateEncryptor())
                {
                    var arr = encrypter.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(arr);
                }
            }
        }

        public static string Decrypt(string encodedStr)
        {
            // Common.Log("Decrypt: " + encodedStr);
            var data = Convert.FromBase64String(encodedStr);
            using (var csp = new AesCryptoServiceProvider())
            {
                csp.KeySize = 256;
                csp.BlockSize = 128;
                csp.Key = key;
                csp.Padding = PaddingMode.PKCS7;
                csp.Mode = CipherMode.ECB;

                using (var decrypter = csp.CreateDecryptor())
                {
                    var arr = decrypter.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.UTF8.GetString(arr);
                }
            }
        }

        public static string DecryptBase64(string base64)
        {
            var mBytes = Convert.FromBase64String(base64);
            var domain = ASCIIEncoding.ASCII.GetString(mBytes);
            return domain;
        }
    }
}