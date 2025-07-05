using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Members.Login;
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

namespace Application.Members.Register
{
    internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        public RegisterCommandHandler(IMemberRepository memberRepository,IUnitOfWork unitOfWork,    IPasswordService passwordService)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
        }

        public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Result<Email> emailResult = Email.Create(request.Email);
            
            if(emailResult.IsFailure) {
                return Result.Failure<Guid>(emailResult.Error);
            }

            Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
            Result<LastName> lastNameResult = LastName.Create(request.LastName);
            Result<Password> passwordResult = Password.Create(request.Password);


            if (passwordResult.IsFailure)
            {
                return Result.Failure<Guid>(passwordResult.Error);
            }

            if (!await _memberRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
            {
                return Result.Failure<Guid>(DomainErrors.Member.EmailAlreadyInUse);
            }

            var member = Member.Create(
                Guid.NewGuid(),
                emailResult.Value,
                firstNameResult.Value,
                lastNameResult.Value
                );

            var hashedResult = _passwordService.HashPassword(member,request.Password);
            member.SetPassword(hashedResult.Value);

            _memberRepository.Add(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return member.Id;
        }
    }
}
