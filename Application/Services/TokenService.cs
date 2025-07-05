using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public static class TokenService
    {
        public static string HashToken(this string token)
        {
            using var sha256 = SHA256.Create();

            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var tokenHash = sha256.ComputeHash(tokenBytes);
            var tokenHashBase64 = Convert.ToBase64String(tokenHash);

            return tokenHashBase64;
        }

        public static string GenerateResetToken()
        {
            var rng = RandomNumberGenerator.GetInt32(100000, 1000000);
            var token = rng.ToString();
            return token;
        }
        
    }
}
