using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Passwords.ForgotPassword
{
    public record ForgotPasswordCommand(string Email):ICommand<string>;

}
