using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    //Encrypts all saved filed
    class XOR_Encryption
    {
        //Uses XOR Encryption to Encrypt and decrypt. This kind of encryption is undone by
        //applying the same method again.
        //Source: https://www.codingame.com/playgrounds/11117/simple-encryption-using-c-and
        //-xor-technique
        public string EncryptDecrypt(string text, int encryptionKey)
        {
            if (text == null)
            {
                return text;
            }
            //This string will feed the encryption characters to the output
            StringBuilder encryptedInput = new StringBuilder(text);
            
            //This is what will be returned back
            StringBuilder encryptedOutput = new StringBuilder(text.Length);

            //Used as the placeholder for each character that is fed to the output string.
            char character;

            //Iterates through each character in the input, encrypts it, then saves it to the 
            //output string.
            for (int iCount = 0; iCount < text.Length; iCount++)
            {
                character = encryptedInput[iCount];
                character = (char)(character ^ encryptionKey);
                encryptedOutput.Append(character);
            }
            return encryptedOutput.ToString();
        }
    }
}
