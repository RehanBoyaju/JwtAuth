using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.Login
{
    //Contains the information that is required to authenticate our user 
    public record LoginCommand(string Email,string Password) : ICommand<string>;

    //return type is string bcs it represents a JWT web token
    //Can also pass in a Password and then Hash it
}
