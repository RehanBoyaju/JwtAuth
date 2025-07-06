using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repository;
using Domain.Shared;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.Login
{
    internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IJWTProvider _jwtProvider;
        private readonly IPasswordService _passwordService;
        public LoginCommandHandler(IMemberRepository memberRepository,IJWTProvider jwtProvider,IPasswordService passwordService)
        {
            _memberRepository = memberRepository;
            _jwtProvider = jwtProvider;
            _passwordService = passwordService;
        }

         public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            // Get member
            var email = Email.Create(request.Email);

            if (email.IsFailure)
            {
                return Result.Failure<string>(email.Error);
            }

            var password = Domain.ValueObjects.Password.Create(request.Password);

            if (password.IsFailure)
            {
                return Result.Failure<string>(password.Error);
            }

            //can also check if its a success result


            Member? member = await _memberRepository.GetByEmailAsync(email.Value,cancellationToken);

            if (member is null)
            {
                return Result.Failure<string>(DomainErrors.Member.InvalidCredentials);
            }

            //can also check for invalid Password

            var verifyPassword = await _passwordService.VerifyPasswordAsync(member,password.Value.Value);

            if (verifyPassword.IsFailure)
            {
                return Result.Failure<string>(verifyPassword.Error);
            }

           
            // If exist Generate JWT and return it

            string token = _jwtProvider.Generate(member);

            return token;

        }
    }
}
