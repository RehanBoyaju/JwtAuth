using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Passwords.VerifyToken
{
    public record  VerifyTokenRequest (string Email,string Token);
}
