using System.Security.Cryptography;

namespace TripPlanner.Infrastructure;

public static class EncryptionHelper
{
    public static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        // Create an Aes object with the specified key and IV.
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        // Create a new MemoryStream object to contain the encrypted bytes.
        using var memoryStream = new MemoryStream();
        // Create a CryptoStream object to perform the encryption.
        using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        // Encrypt the plaintext.
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(plainText);
        }

        var encrypted = memoryStream.ToArray();

        return encrypted;
    }
    
    public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Create an Aes object with the specified key and IV.
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        // Create a new MemoryStream object to contain the decrypted bytes.
        using var memoryStream = new MemoryStream(cipherText);
        // Create a CryptoStream object to perform the decryption.
        using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        // Decrypt the ciphertext.
        using var streamReader = new StreamReader(cryptoStream);
        var decrypted = streamReader.ReadToEnd();

        return decrypted;
    }
}