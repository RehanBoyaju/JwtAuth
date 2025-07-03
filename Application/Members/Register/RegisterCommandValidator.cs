using Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.Register
{
    internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() { 
            RuleFor(x=>x.Email).NotEmpty().EmailAddress();  
            RuleFor(x=>x.FirstName).NotEmpty().MaximumLength(FirstName.MaxLength);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(LastName.MaxLength);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(Password.MinLength);
        }
    }
}
