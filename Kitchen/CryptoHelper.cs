using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Cryptography;
using System.Text; //Encoding;
using System.IO;

using System.Diagnostics;


namespace Kitchen
{

    public static class CryptoHelper
	{

        public static CryptoRandom _random = new CryptoRandom();


        /// <summary>
        /// Overload for encrypt info
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="sKey"></param>
        /// <param name="fixedIV"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string encryptAESInfo(string plainText, string sKey)
        {
            /*
            string[] sKeyArry = sKey.Split('-');
            string NewSKey = sKeyArry[3] + sKeyArry[1] + sKeyArry[2] + sKeyArry[1] + "12Retai1";
            */
            string part1= sKey.Substring(0, 12);
            string part2= sKey.Substring(12, 16);
            string NewSKey = part2 + part1 + "12Retai1";

            return encryptAES(plainText, NewSKey);
        }

        //encrypt AES - random IV (16byte) parsed at the beginning of the base64 string
        public static string encryptAES(string plainText, string sKey,string fixedIV = null)
        {
            byte[] Key = ComputeHash(sKey); //SHA256


            string MD5Key = "";
            if (fixedIV != null)
            {
                MD5Key = fixedIV;
            }
            else
            {
                //RANDOM IV
                MD5Key = Convert.ToString(new DateTime().Ticks + _random.Next());
                
            }

            byte[] IV = ComputeHash(MD5Key, "MD5"); //MD5 - must be 128bit for AES in this IV = Block Size
             

            //concat IV in front of the return ciphered text
            return Convert.ToBase64String(IV.Concat(encryptStringToBytes_AES(plainText, Key, IV)).ToArray());
        }

        /// <summary>
        /// Decrypt personal info
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string decryptAESInfo(string plainText, string sKey)
        {
            //string[] sKeyArry = sKey.Split('-');
            //string NewSKey = sKeyArry[3] + sKeyArry[1] + sKeyArry[2] + sKeyArry[1] + "12Retai1";

            string part1 = sKey.Substring(0, 12);
            string part2 = sKey.Substring(12, 16);
            string NewSKey = part2 + part1 + "12Retai1";
            return decryptAES(plainText, NewSKey);
        }

        //decrypt AES - use the first 16bytes as IV
        public static string decryptAES(string cipherText_str, string sKey)
        {
            byte[] cipherText = null;
            byte[] Key = ComputeHash(sKey); //SHA256
            byte[] IV = null;
            byte[] tmp;

            //using - linq
            tmp = Convert.FromBase64String(cipherText_str);

            IV = tmp.Take(16).ToArray();
            cipherText = tmp.Skip(16).Take(tmp.Length - 16).ToArray();
            tmp = null;

            return decryptStringFromBytes_AES(cipherText, Key, IV);
        }

        public static byte[] encryptStringToBytes_AES(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");

            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");


            // Declare the streams used
            // to encrypt to an in memory
            // array of bytes.
            MemoryStream msEncrypt = null;
            CryptoStream csEncrypt = null;
            StreamWriter swEncrypt = null;

            // Declare the Aes object
            // used to encrypt the data.
            Aes aesAlg = null;

            try
            {
                // Create an Aes object
                // with the specified key and IV.
                aesAlg = Aes.Create();
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                msEncrypt = new MemoryStream();
                csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                swEncrypt = new StreamWriter(csEncrypt);

                //Write all data to the stream.
                swEncrypt.Write(plainText);

            }
            finally
            {
                // Clean things up.

                // Close the streams.
                if (swEncrypt != null)
                    swEncrypt.Close();
                if (csEncrypt != null)
                    csEncrypt.Close();
                if (msEncrypt != null)
                    msEncrypt.Close();

                // Clear the Aes object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return msEncrypt.ToArray();

        }

        public static string decryptStringFromBytes_AES(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // TDeclare the streams used
            // to decrypt to an in memory
            // array of bytes.
            MemoryStream msDecrypt = null;
            CryptoStream csDecrypt = null;
            StreamReader srDecrypt = null;

            // Declare the Aes object
            // used to decrypt the data.
            Aes aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // Create an Aes object
                // with the specified key and IV.
                aesAlg = Aes.Create();
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                msDecrypt = new MemoryStream(cipherText);
                csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                srDecrypt = new StreamReader(csDecrypt);

                // Read the decrypted bytes from the decrypting stream
                // and place them in a string.
                plaintext = srDecrypt.ReadToEnd();
            }
            finally
            {
                // Clean things up.

                // Close the streams.
                if (srDecrypt != null)
                    srDecrypt.Close();
                if (csDecrypt != null)
                    csDecrypt.Close();
                if (msDecrypt != null)
                    msDecrypt.Close();

                // Clear the Aes object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;

        }

        //hexdecimal string to byte array - 2 char to 1 byte
        public static byte[] StringToByteArray(string hexValueInString)
        {
            int NumberChars = hexValueInString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2) bytes[i / 2] = System.Convert.ToByte(hexValueInString.Substring(i, 2), 16);
            return bytes;
        }

        //byte array to hex string, to out hash as string
        public static string HexStr(byte[] bytes, bool lowercase=false)
        {
            string hexString = string.Empty;
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString += bytes[i].ToString("X2");
            }
            if (lowercase) hexString = hexString.ToLower();

            return hexString;
        }

        public static T ComputeHash<T>(string plainText, string hashAlgorithm = null)
        {
            return (T) (object) HexStr(ComputeHash(plainText,hashAlgorithm));
        }

        //function to calculate hash - default SHA256
        public static byte[] ComputeHash(string plainText, string hashAlgorithm=null)
        {

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Because we support multiple hashing algorithms, we must define
            // hash object as a common (abstract) base class. We will specify the
            // actual hashing algorithm class later during object creation.
            HashAlgorithm hash;

            // Make sure hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Initialize appropriate hashing algorithm class.
            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hash = new SHA1Managed();
                    break;

                case "SHA256":
                default:

                    hash = new SHA256Managed();
                    break;

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                case "MD5":
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);

            hash.Dispose();

            // Return the result.
            return hashBytes;

        }// end ComputeHash

        //UTF8 to BASE64 encoder / decoder
        public static string Encode64(string str)
        {
            byte[] buffer;
            string result = "Error!";
            try
            {
                buffer = System.Text.Encoding.UTF8.GetBytes(str);
                result = Convert.ToBase64String(buffer);
            }
            catch
            //catch (Exception e)
            {
                // throw new Exception("Error Encode64" + e.Message);
            }
            return result;
        }

        public static string Decode64(string str)
        {
            byte[] buffer;
            string result = "Error!";
            try
            {
                buffer = Convert.FromBase64String(str);
                result = System.Text.Encoding.UTF8.GetString(buffer);
            }
            catch
            //catch (Exception e)
            {
                //throw new Exception("Error in Decode64" + e.Message);
            }

            return result;
        }


        //source code 
        //http://www.obviex.com/samples/Code.aspx?Source=HashCS&Title=Hashing%20Data&Lang=C%23
        public static byte[] genSalt(int saltSize)
        {
            // Allocate a byte array, which will hold the salt.
            byte[] saltBytes = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(saltBytes);
            rng.Dispose();
            return saltBytes;
        }


        //adapted from SOURCE http://www.chilkatsoft.com/p/p_330.asp

        // 192-bit (three-key) 3DES (Triple-DES) (ECB mode)
        // key should be 24 bytes.
        public static string TripleDesEncrypt(string plainText, string key)
        {

            // Create a new 3DES key.
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

            //pad trailing spaces
            string spaces = "        ";
            plainText = plainText + spaces.Substring(0, plainText.Length % 8);
            string paddedText = plainText + spaces.Substring(0, plainText.Length % 8);

            //convert UTF8 string to byte[]

            byte[] plainTextBytes = Encoding.ASCII.GetBytes(paddedText);
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] enc;

            try
            {
                // Set the KeySize = 192 for 168-bit DES encryption.
                // The msb of each byte is a parity bit, so the key length is actually 168 bits.
                des.KeySize = 192;
                des.Key = keyBytes;
                des.Mode = CipherMode.ECB;

                des.Padding = PaddingMode.None;

                //des.Padding = PaddingMode.PKCS7;

                ICryptoTransform ic = des.CreateEncryptor();

                enc = ic.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);

            }
            finally
            {
                des.Dispose();
            }

            string encString = HexStr(enc, true);
            return encString;
        }


        public static string TripleDesDecrypt(string CypherText, string key)
        {
            byte[] b = StringToByteArray(CypherText);
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] output;

            TripleDES des = new TripleDESCryptoServiceProvider();
            try
            {
                des.KeySize = 192;
                des.Key = keyBytes;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.None;

                //des.Padding = PaddingMode.PKCS7;

                ICryptoTransform Decryptor = des.CreateDecryptor();
                output = Decryptor.TransformFinalBlock(b, 0, b.Length);
            }
            finally
            {
                des.Dispose();
            }
            return Encoding.ASCII.GetString(output);
        }


        public static string encryptClient(string text, string pwd)
        {
            return Rijndael(text, pwd);
        }

        public static string decryptClient(string cipherText_str, string key_str)
        {
            string result = null;
            byte[] tmp;
            byte[] IV;
            byte[] cipherText;

            try
            {
                tmp = Convert.FromBase64String(cipherText_str);
                IV = tmp.Take(16).ToArray();
                cipherText = tmp.Skip(16).Take(tmp.Length - 16).ToArray();
                byte[] key = StringToByteArray(key_str);
                tmp = null;

                result = DecryptStringFromBytes(cipherText, key, IV);
            }
            catch
            {

            }

            return result;
        }

        public static string Rijndael(string plaintext, string pwd)
        {
            // Create a new instance of the RijndaelManaged
            // class.  This generates a new key and initialization
            // vector (IV).
            string ciphertext = null;
            try
            {

                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {

                    myRijndael.BlockSize = 128;
                    myRijndael.Mode = CipherMode.CBC;
                    myRijndael.Padding = PaddingMode.PKCS7;
                    //context.Response.Write(String.Format("Valid Key Size:   {0} <br>", myRijndael.ValidKeySize(256).ToString() ));


                    byte[] Key = CryptoHelper.ComputeHash(pwd);
                    byte[] IV = CryptoHelper.ComputeHash(Convert.ToString(new DateTime().Ticks), "MD5");

                    //context.Response.Write(String.Format("Key:   {0} <br>", CryptoHelper.HexStr(Key)));
                    //context.Response.Write(String.Format("IV:   {0} <br>", CryptoHelper.HexStr(IV)));
                    Debug.WriteLine(String.Format("Key:   {0} <br>", CryptoHelper.HexStr(Key)));
                    Debug.WriteLine(String.Format("IV:   {0} <br>", CryptoHelper.HexStr(IV)));

                    myRijndael.IV = IV;
                    myRijndael.Key = Key;

                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes(plaintext, myRijndael.Key, myRijndael.IV);

                    // Decrypt the bytes to a string.
                    //string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);

                    //Display the original data and the decrypted data.
                    Debug.WriteLine(String.Format("Original:   {0} <br>", plaintext));
                    Debug.WriteLine(String.Format("encrypted:   {0} <br>", CryptoHelper.HexStr(encrypted)));
                    Debug.WriteLine(String.Format("base64:   {0} <br>", Convert.ToBase64String(encrypted)));
                    //context.Response.Write(String.Format("Original:   {0} <br>", original));
                    //context.Response.Write(String.Format("encrypted:   {0} <br>", CryptoHelper.HexStr (encrypted) ) );
                    //context.Response.Write(String.Format("base64:   {0} <br>", Convert.ToBase64String (encrypted) ) );
                    //context.Response.Write(String.Format("Round Trip: {0} <br>", roundtrip));
                }

            }
            catch 
            {
                //context.Response.Write(String.Format("Error: {0}", e.Message));
            }

            return ciphertext;
        }

        public static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;


        }

        public static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

	}


    public class CryptoRandom
    {
        private static RandomNumberGenerator r;

        ///<summary>
        /// Creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.
        ///</summary>
        public CryptoRandom()
        {
            r = RandomNumberGenerator.Create();
        }

        ///<summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        ///</summary>
        ///<param name=”buffer”>An array of bytes to contain random numbers.</param>

        /*
        public override void GetBytes(byte[] buffer)
        {
            r.GetBytes(buffer);
        }
        */

        ///<summary>
        /// Returns a random number between 0.0 and 1.0.
        ///</summary>
        public double NextDouble()
        {
            byte[] b = new byte[4];
            r.GetBytes(b);
            return (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
        }

        ///<summary>
        /// Returns a random number within the specified range.
        ///</summary>
        ///<param name=”minValue”>The inclusive lower bound of the random number returned.</param>
        ///<param name=”maxValue”>The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        public int Next(int minValue, int maxValue)
        {
            return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
        }

        ///<summary>
        /// Returns a nonnegative random number.
        ///</summary>
        public int Next()
        {
            return Next(0, Int32.MaxValue);
        }

        ///<summary>
        /// Returns a nonnegative random number less than the specified maximum
        ///</summary>
        ///<param name=”maxValue”>The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

    }


}