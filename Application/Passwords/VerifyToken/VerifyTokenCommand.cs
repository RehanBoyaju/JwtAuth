﻿using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Passwords.VerifyToken
{
    public record  VerifyTokenCommand (string Email,string Token) : ICommand;
}
