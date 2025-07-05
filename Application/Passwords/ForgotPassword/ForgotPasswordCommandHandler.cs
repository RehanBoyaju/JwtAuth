using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Helpers;
using Application.Services;
using Domain.Errors;
using Domain.Repository;
using Domain.Shared;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Passwords.ForgotPassword
{
    internal class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, string>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IPasswordService _passwordService;
        private readonly IEmailService _emailService;
        public ForgotPasswordCommandHandler(IMemberRepository memberRepository,IPasswordService passwordService,IEmailService emailService)
        {
            _memberRepository = memberRepository;
            _passwordService = passwordService;
            _emailService = emailService;
        }

        public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);

            if (emailResult.IsFailure)
            {
                return Result.Failure<string>(emailResult.Error);
            }
            
            var member = await _memberRepository.GetByEmailAsync(emailResult.Value,cancellationToken);
            if (member == null)
            {
                return Result.Failure<string>(DomainErrors.Member.InvalidCredentials);
            }
            


            var token = TokenService.GenerateResetToken();

            await _passwordService.HashResetToken(member,token);

            
            await _emailService.SendForgotPasswordEmail(member.Email.Value,token);

            var maskedEmail = member.Email.Value.MaskEmail();

            return Result.Success($"Password reset email sent to {maskedEmail}");
            
        }
    }
}

