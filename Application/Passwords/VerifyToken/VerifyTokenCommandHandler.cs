using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repository;
using Domain.Shared;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Passwords.VerifyToken
{
    public class VerifyTokenCommandHandler : ICommandHandler<VerifyTokenCommand>
    {
        private readonly IPasswordService _passwordService;
        private readonly IMemberRepository _memberRepository;
        public VerifyTokenCommandHandler(IPasswordService passwordService,IMemberRepository memberRepository)
        {
            _passwordService = passwordService;
            _memberRepository = memberRepository;
        }

        public async Task<Result> Handle(VerifyTokenCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
            {
                return Result.Failure(DomainErrors.Email.InvalidFormat);
            }
            var member = await _memberRepository.GetByEmailAsync(emailResult.Value);
            if(member is null)
            {
                return Result.Failure(DomainErrors.Member.InvalidCredentials);
            }
            var verifyTokenResult = await _passwordService.VerifyTokenAsync(member,request.Token,cancellationToken);

            if (verifyTokenResult.IsFailure)
            {
                return Result.Failure(verifyTokenResult.Error);
            }


            return Result.Success();
            
        }
    }
}
