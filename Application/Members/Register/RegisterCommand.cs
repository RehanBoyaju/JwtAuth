﻿using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Members.Register
{
    public record RegisterCommand(string Email,string FirstName,string LastName,string Password) : ICommand<Guid>;

}
