using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShop.Application.Security
{
    public static class PasswordHelper
    {

        //Encrypt using MD5   
        public static string EncodePasswordMd5(this string pass)
        {
            byte[] originalBytes;
            byte[] encodedBytes;
            MD5 md5;

            // Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);

            // Convert encoded bytes back to a 'readable' string   
            return BitConverter.ToString(encodedBytes).Replace("-", "");
        }

        // Verify if a plain password matches the hashed password
        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            // Hash the plain password
            string hashedInput = plainPassword.EncodePasswordMd5();

            // Compare the hashed input with the stored hashed password
            return string.Equals(hashedInput, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

    }
}
