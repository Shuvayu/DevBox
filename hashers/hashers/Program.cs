using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hashers
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "";
            int selectedValue = 0;

            Console.WriteLine("Select Hashing Algorithm");
            Console.WriteLine("1:MD5");
            Console.WriteLine("2:SHA256");
            source = Console.ReadLine();
            try
            {
                Int32.TryParse(source, out selectedValue);
            }
            catch (Exception)
            {
                Console.WriteLine("Sigh !!!");               
            }

            switch (selectedValue)
            {
                case 1:
                    UsingMD5();
                    break;
                case 2:
                    Console.WriteLine("Oops : SHA256 no implemented yet!");
                    break;
                default:
                    break;
            }

            Console.ReadLine();
        }

        static void UsingMD5()
        {
            using (MD5 md5Hash = MD5.Create())
            {
                Console.WriteLine("You have selected MD5");
                Console.WriteLine("Enter a string to be converted.");

                string source = Console.ReadLine();
                string hash = GetMd5Hash(md5Hash, source);

                Console.WriteLine("The MD5 hash of " + source + " is: " + hash + ".");

                Console.WriteLine("Verifying the hash...");

                if (VerifyMd5Hash(md5Hash, source, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }

                System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Dev\\GNS-Apps\\hashers\\hashers\\test.txt", true);
                file.WriteLine(hash);

                file.Close();
            }
        }

        static void UsingSHA256()
        {
            // Initialize the keyed hash object.
            using (HMACSHA256 hmac = new HMACSHA256())
            {
                

            }
            return;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
