using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var passwordText = "123456789101112";
            var salt = Encoding.UTF8.GetBytes("password12345689101112");
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = GeneratePasswordHashUsingSalt(passwordText, salt);
            stopwatch.Stop();

            Console.WriteLine($"Elapsed Milliseconds: {stopwatch.ElapsedMilliseconds}ms");
            Console.ReadLine();
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            var iterate = 10000;
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, 16 * sizeof(byte));
            Buffer.BlockCopy(hash, 0, hashBytes, 16, 20 * sizeof(byte));

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }
    }
}
