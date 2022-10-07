using System.IO;

namespace SecureChat.Rsa
{
    public class RsaKeyGenerator
    {
        public int KeySize { get; private set; }

        public RsaKeyGenerator(int keySize)
        {
            KeySize = keySize;
        }

        public void GenerateKeyPairToFiles(string privateKeyPath, string publicKeyPath)
        {
            var keygen = new SshKeyGenerator.SshKeyGenerator(KeySize);

            var privateKey = keygen.ToPrivateKey();
            using StreamWriter privateKeyStreamWriter = new(privateKeyPath);
            privateKeyStreamWriter.WriteLine(privateKey);

            var publicKey = keygen.ToPublicKey();
            using StreamWriter publicKeyStreamWriter = new(publicKeyPath);
            publicKeyStreamWriter.WriteLine(publicKey);
        }
    }
}