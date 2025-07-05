using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class StringExtensions
    {
        public static string MaskEmail(this string email)
        {
            var parts = email.Split('@');
            if(parts.Length != 2)
            {
                throw new ArgumentException($"Email:{email} is not in valid format");
            }
            var username = parts[0];
            var domain = parts[1];

            var maskedUsername = username.Length<=2?
                new string('*',username.Length)
                :username.Substring(0, 2) + new string('*', username.Length - 2);

            return $"{maskedUsername}@{domain}";

        }
        
    }
}
