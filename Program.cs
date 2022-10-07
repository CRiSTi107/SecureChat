using System;
using SecureChat.Rsa;


namespace secure_chat
{
    class Program
    {
        static void Main(string[] args)
        {
            int keySize;

            Console.WriteLine("Enter keysize: ");
            keySize = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Generating keys...");
            RsaKeyGenerator rsaKeyGenerator = new RsaKeyGenerator(keySize);
            rsaKeyGenerator.GenerateKeyPairToFiles("private.pem", "public.pub");
            Console.WriteLine("Done generating keys!");

            RsaServiceProvider rsaServiceProvider = new RsaServiceProvider("private.pem", "public.pub");

            Console.WriteLine("Enter unencrypted message: ");
            string plainMessage = Console.ReadLine();
            byte[] plainMessageBytes = System.Text.Encoding.UTF8.GetBytes(plainMessage);

            byte[] encryptedMessageBytes = rsaServiceProvider.Encrypt(plainMessageBytes);
            string encryptedMessage = Convert.ToBase64String(encryptedMessageBytes);
            Console.WriteLine($"Encrypted message: {encryptedMessage}");

            byte[] decryptedMessageBytes = rsaServiceProvider.Decrypt(encryptedMessageBytes);
            string decryptedMessage = System.Text.Encoding.UTF8.GetString(decryptedMessageBytes);
            Console.WriteLine($"Decrypted message: {decryptedMessage}");
        }

    }
}
