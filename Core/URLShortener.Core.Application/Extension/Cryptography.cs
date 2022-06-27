using System;
using System.Security.Cryptography;
using System.Text;

namespace URLShortener.Core.Application.Extension
{
    public static class Cryptography
    {
        public static string ToSHA512(this string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA512.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
