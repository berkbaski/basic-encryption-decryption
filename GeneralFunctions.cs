using System;
using System.Security.Cryptography;
using System.Text;

public static class GeneralFunctions
{
    private const string EncryptionKey = "A!9HHhi%XjjYY4YP2@Nob009X";
    public static string Encrypt(string text)
    {
        var md5 = new MD5CryptoServiceProvider();
        var tdes = new TripleDESCryptoServiceProvider
        {
            Key = md5.ComputeHash(Encoding.UTF8.GetBytes(EncryptionKey)),
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        var transform = tdes.CreateEncryptor();
        var textBytes = Encoding.UTF8.GetBytes(text);
        var bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);

        return Convert.ToBase64String(bytes, 0, bytes.Length);
    }

    public static string Decrypt(string cipher)
    {
        var md5 = new MD5CryptoServiceProvider();
        var tdes = new TripleDESCryptoServiceProvider
        {
            Key = md5.ComputeHash(Encoding.UTF8.GetBytes(EncryptionKey)),
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        var transform = tdes.CreateDecryptor();
        var cipherBytes = Convert.FromBase64String(cipher);
        var bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(bytes);
    }
}
