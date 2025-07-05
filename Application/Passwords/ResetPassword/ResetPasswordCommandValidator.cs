using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;


namespace Application.Passwords.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>  
    {
        public ResetPasswordCommandValidator() { 
            RuleFor(x=>x.Email).NotEmpty();
            RuleFor(x=>x.NewPassword).NotEmpty().MinimumLength(Password.MinLength);
            RuleFor(x=>x.Token).NotEmpty().Length(6);
        }
    }
}
