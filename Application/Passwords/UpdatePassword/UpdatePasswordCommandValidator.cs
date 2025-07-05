using Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Passwords.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();
            RuleFor(x => x.OldPassword).NotEmpty().MinimumLength(Password.MinLength);
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(Password.MinLength);
        }
    }
}
