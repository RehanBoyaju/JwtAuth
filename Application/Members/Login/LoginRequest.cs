using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.Login
{
    /// <summary>
    /// This is going to map to LoginCommand 
    /// </summary>
    /// <param name="Email"></param>
    public record LoginRequest(string Email,string Password);
}
