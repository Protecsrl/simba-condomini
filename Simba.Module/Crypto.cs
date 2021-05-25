using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CAMS.Module
{
    public static class Crypto
    {
        public const string CamsSalt = "XXXXXXXXXXXXXXXXXXXXXXX";

        private static readonly byte[] IVa = new byte[] { 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x11, 0x11, 0x12, 0x13, 0x14, 0x0e, 0x16, 0x17 };


        public static string Encrypt(this string text, string salt)
        {
            try
            {
                using (Aes aes = new AesManaged())
                {
                    var deriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetString(IVa, 0, IVa.Length), Encoding.UTF8.GetBytes(salt));
                    aes.Key = deriveBytes.GetBytes(128 / 8);
                    aes.IV = aes.Key;
                    using (var encryptionStream = new MemoryStream())
                    {
                        using (var encrypt = new CryptoStream(encryptionStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            var cleanText = Encoding.UTF8.GetBytes(text);
                            encrypt.Write(cleanText, 0, cleanText.Length);
                            encrypt.FlushFinalBlock();
                        }

                        var encryptedData = encryptionStream.ToArray();
                        var encryptedText = Convert.ToBase64String(encryptedData);


                        return encryptedText;
                    }
                }
            }
            catch
            {
                return String.Empty;
            }
        }

        public static string Decrypt(this string text, string salt)
        {
            try
            {
                using (Aes aes = new AesManaged())
                {
                    var deriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetString(IVa, 0, IVa.Length), Encoding.UTF8.GetBytes(salt));
                    aes.Key = deriveBytes.GetBytes(128 / 8);
                    aes.IV = aes.Key;

                    using (var decryptionStream = new MemoryStream())
                    {
                        using (var decrypt = new CryptoStream(decryptionStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            var encryptedData = Convert.FromBase64String(text);


                            decrypt.Write(encryptedData, 0, encryptedData.Length);
                            decrypt.Flush();
                        }

                        var decryptedData = decryptionStream.ToArray();
                        var decryptedText = Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);


                        return decryptedText;
                    }
                }
            }
            catch(Exception ex)
            {
                string err = ex.Message;

                return String.Empty;
            }
        }
    }
}
