using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Passwords.UpdatePassword
{
    public record UpdatePasswordCommand(Guid MemberId,string OldPassword, string NewPassword) : ICommand;
}
