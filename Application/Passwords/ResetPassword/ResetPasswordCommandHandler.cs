using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repository;
using Domain.Shared;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Passwords.ResetPassword
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, string>
    {
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMemberRepository _memberRepository;
        public ResetPasswordCommandHandler(IPasswordResetTokenRepository passwordResetTokenRepository, IPasswordService passwordService, IMemberRepository memberRepository)
        {
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _passwordService = passwordService;
            _memberRepository = memberRepository;
        }


        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);

            if (emailResult.IsFailure)
            {
                return Result.Failure<string>(emailResult.Error);
            }

            var passwordResult = Password.Create(request.NewPassword);
            if (passwordResult.IsFailure)
            {
                return Result.Failure<string>(passwordResult.Error);
            }

            var member = await _memberRepository.GetByEmailAsync(emailResult.Value);
            if(member is null)
            {
                return Result.Failure<string>(DomainErrors.Member.InvalidCredentials);
            }

            var tokenResult =await _passwordService.VerifyTokenAsync(member,request.Token,cancellationToken);

            if(tokenResult.IsFailure)
            {
                return Result.Failure<string>(tokenResult.Error);
            }

            var resetResult = await _passwordService.ResetPasswordAsync(member,passwordResult.Value.Value,tokenResult.Value,cancellationToken);

            if(resetResult.IsFailure)
            {
                return Result.Failure<string>(resetResult.Error);
            }

            return Result.Success<string>("Password reset successfully");
            
        }
    }
}
