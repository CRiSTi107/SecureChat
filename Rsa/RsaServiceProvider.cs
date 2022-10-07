using System.IO;
using PemUtils;
using System.Security.Cryptography;

namespace SecureChat.Rsa
{
    public class RsaServiceProvider
    {
        public string PrivateKeyPath { get; private set; }
        public string PublicKeyPath { get; private set; }

        private RSAParameters PrivateKeyParameters;
        private RSAParameters PublicKeyParameters;

        public RsaServiceProvider(string privateKeyPath, string publicKeyPath)
        {
            PrivateKeyPath = privateKeyPath;
            PublicKeyPath = publicKeyPath;

            LoadKeys();
        }

        private void LoadKeys()
        {
            LoadPrivateKey();
            LoadPublicKey();
        }

        private void LoadPrivateKey()
        {
            using var privateKeyStream = File.OpenRead(PrivateKeyPath);
            using var privateKeyReader = new PemReader(privateKeyStream);

            PrivateKeyParameters = privateKeyReader.ReadRsaKey();
        }

        private void LoadPublicKey()
        {
            using var publicKeyStream = File.OpenRead(PublicKeyPath);
            using var publicKeyReader = new PemReader(publicKeyStream);

            PublicKeyParameters = publicKeyReader.ReadRsaKey();
        }

        public byte[] Encrypt(byte[] dataToEncrypt)
        {
            RSA rsa = RSA.Create(PublicKeyParameters);
            byte[] encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA512);

            return encryptedData;
        }

        public byte[] Decrypt(byte[] dataToDecrypt)
        {
            RSA rsa = RSA.Create(PrivateKeyParameters);
            byte[] decryptedData = rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA512);

            return decryptedData;
        }
    }
}