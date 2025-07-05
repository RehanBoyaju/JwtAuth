using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Passwords.VerifyToken
{
    public class VerifyTokenCommandValidator : AbstractValidator<VerifyTokenCommand>
    {
        public VerifyTokenCommandValidator() { 
            RuleFor(x=>x.Email).NotEmpty();
            RuleFor(x=>x.Token).Length(6);
        }
    }
}
