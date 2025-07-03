using Application.Abstractions;
using Domain.Entities;
using Domain.Errors;
using Domain.Repository;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<Member> _hasher;
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PasswordService(IPasswordHasher<Member> hasher, IMemberRepository repository, IUnitOfWork unitOfWork)
        {
            _hasher = hasher;
            _memberRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> ChangePasswordAsync(Member member, string oldPassword, string newPassword,CancellationToken cancellationToken = default)
        {
            var verify = await VerifyPasswordAsync(member,oldPassword);
            if (!verify)
            {
                return Result.Failure(DomainErrors.Member.InvalidCredentials);
            }
            var newHash = HashPassword(member, newPassword);
            member.SetPassword(newHash);

            _memberRepository.Update(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

       

        public string HashPassword(Member member, string password)
        {
            return _hasher.HashPassword(member, password);
        }

        public async Task<bool> VerifyPasswordAsync(Member member, string providedPassword)
        {
            var verify = _hasher.VerifyHashedPassword(member, member.HashedPassword, providedPassword);
            if(verify == PasswordVerificationResult.Success)
            {
                return true;
            }
            if(verify == PasswordVerificationResult.SuccessRehashNeeded)
            {
                await ChangePasswordAsync(member,providedPassword,providedPassword);
                return true;
            }

            return false;
        }
    }
}
