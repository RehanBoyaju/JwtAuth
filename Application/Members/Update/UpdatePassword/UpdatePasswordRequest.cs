using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.Update.UpdatePassword
{
    public record UpdatePasswordRequest(string OldPassword,string NewPassword);
}
