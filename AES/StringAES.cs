using System.Text;

namespace AESik.AES;

public class StringAES : AES
{
    public StringAES(string key) : base(Encoding.UTF8.GetBytes(key.PadRight(16, '\0')))
    {
    }

    public string Encrypt(string plaintext)
    {
        byte[] input = Encoding.UTF8.GetBytes(plaintext);
        int paddingLength = 16 - (input.Length % 16);
        byte[] paddedInput = new byte[input.Length + paddingLength];
        Array.Copy(input, paddedInput, input.Length);
        for (int i = 0; i < paddingLength; i++)
            paddedInput[input.Length + i] = (byte)paddingLength; 

        byte[] encrypted = new byte[paddedInput.Length];
        for (int i = 0; i < paddedInput.Length; i += 16)
        {
            byte[] block = new byte[16];
            Array.Copy(paddedInput, i, block, 0, 16);
            byte[] encryptedBlock = base.Encrypt(block);
            Array.Copy(encryptedBlock, 0, encrypted, i, 16);
        }
        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string ciphertext)
    {
        byte[] input = Convert.FromBase64String(ciphertext);
        byte[] decrypted = new byte[input.Length];

        for (int i = 0; i < input.Length; i += 16)
        {
            byte[] block = new byte[16];
            Array.Copy(input, i, block, 0, 16);
            byte[] decryptedBlock = base.Decrypt(block);
            Array.Copy(decryptedBlock, 0, decrypted, i, 16);
        }

        int paddingLength = decrypted[decrypted.Length - 1];
        byte[] unpaddedOutput = new byte[decrypted.Length - paddingLength];
        Array.Copy(decrypted, unpaddedOutput, unpaddedOutput.Length);
        return Encoding.UTF8.GetString(unpaddedOutput);
    }
}
