using Application.Abstractions.Messaging;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Passwords.ResetPassword
{
    public record ResetPasswordCommand (string Email,string Token,string NewPassword): ICommand<string>;
   
}
