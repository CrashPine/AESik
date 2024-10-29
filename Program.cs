using AESik.AES;

public class Program
{
    public static void Main()
    {
        var key1 = "pass";
        var aes = new StringAES(key1);

        string plaintext = "test";
        string ciphertext = aes.Encrypt(plaintext);
        string decryptedText = aes.Decrypt(ciphertext);

        Console.WriteLine($"Plaintext: {plaintext}");
        Console.WriteLine($"Ciphertext: {ciphertext}");
        Console.WriteLine($"Decrypted text: {decryptedText}");
        
        
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        
        string key = "some pass"; 
        string inputFilePath = "path\\file.name";
        string encryptedFilePath = "path\\file.name";
        string decryptedFilePath = "path\\file.name";

        FileHandlerAES fileHandler = new FileHandlerAES(key);
        
        fileHandler.EncryptFile(inputFilePath, encryptedFilePath);

        fileHandler.DecryptFile(encryptedFilePath, decryptedFilePath);
        
    }
}
