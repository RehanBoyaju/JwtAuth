using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repository;
using Domain.Shared;
using Domain.ValueObjects;

namespace Application.Members.Update.UpdateMember;


internal sealed class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberCommandHandler(
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateMemberCommand request,
        CancellationToken cancellationToken)
    {
        Member? member = await _memberRepository.GetByIdAsync(
            request.MemberId,
            cancellationToken);

        if (member is null)
        {
            return Result.Failure(
                DomainErrors.Member.NotFound(request.MemberId));
        }

        Result<Email> emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Error);
        }

        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);

        if (firstNameResult.IsFailure)
        {
            return Result.Failure(firstNameResult.Error);
        }

        Result<LastName> lastNameResult = LastName.Create(request.LastName);

        if (lastNameResult.IsFailure)
        {
            return Result.Failure(lastNameResult.Error);
        }
        member.Update(
            emailResult.Value,
            firstNameResult.Value,
            lastNameResult.Value);

        _memberRepository.Update(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
