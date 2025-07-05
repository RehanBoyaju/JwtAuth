using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Passwords.ResetPassword
{
    public record ResetPasswordRequest(string Email,string Token, string NewPassword);
    
}
