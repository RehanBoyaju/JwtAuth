using FluentValidation;
using Domain.ValueObjects;

namespace Application.Members.Update.UpdateMember;
public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(x => x.MemberId).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(FirstName.MaxLength);

        RuleFor(x => x.LastName).NotEmpty().MaximumLength(LastName.MaxLength);
    }
}
