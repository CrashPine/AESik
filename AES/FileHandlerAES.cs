using System.Text;

namespace AESik.AES;

public class FileHandlerAES
    {
        private readonly AES _aes;

        public FileHandlerAES(string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.PadRight(16, '\0').Substring(0, 16));
            _aes = new AES(keyBytes);
        }

        public void EncryptFile(string inputFilePath, string outputFilePath)
        {
            byte[] inputData = File.ReadAllBytes(inputFilePath);
            int paddingLength = 16 - (inputData.Length % 16);
            byte[] paddedInput = new byte[inputData.Length + paddingLength];
            Array.Copy(inputData, paddedInput, inputData.Length);
            for (int i = 0; i < paddingLength; i++)
                paddedInput[inputData.Length + i] = (byte)paddingLength; 

            byte[] encryptedData = new byte[paddedInput.Length];
            for (int i = 0; i < paddedInput.Length; i += 16)
            {
                byte[] block = new byte[16];
                Array.Copy(paddedInput, i, block, 0, 16);
                byte[] encryptedBlock = _aes.Encrypt(block);
                Array.Copy(encryptedBlock, 0, encryptedData, i, 16);
            }

            File.WriteAllBytes(outputFilePath, encryptedData);
        }

        public void DecryptFile(string inputFilePath, string outputFilePath)
        {
            byte[] encryptedData = File.ReadAllBytes(inputFilePath);
            byte[] decryptedData = new byte[encryptedData.Length];

            for (int i = 0; i < encryptedData.Length; i += 16)
            {
                byte[] block = new byte[16];
                Array.Copy(encryptedData, i, block, 0, 16);
                byte[] decryptedBlock = _aes.Decrypt(block);
                Array.Copy(decryptedBlock, 0, decryptedData, i, 16);
            }

            int paddingLength = decryptedData[decryptedData.Length - 1];
            byte[] unpaddedOutput = new byte[decryptedData.Length - paddingLength];
            Array.Copy(decryptedData, unpaddedOutput, unpaddedOutput.Length);
            File.WriteAllBytes(outputFilePath, unpaddedOutput);
        }
    }