using Application.Abstractions;
using Application.Helpers;
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
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<Member> _hasher;
        private readonly IMemberRepository _memberRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PasswordService(IPasswordHasher<Member> hasher, IMemberRepository repository, IUnitOfWork unitOfWork, IPasswordResetTokenRepository passwordResetTokenRepository)
        {
            _hasher = hasher;
            _memberRepository = repository;
            _unitOfWork = unitOfWork;
            _passwordResetTokenRepository = passwordResetTokenRepository;
        }

        public async Task<Result> ChangePasswordAsync(Member member, string oldPassword, string newPassword,CancellationToken cancellationToken = default)
        {
            if (oldPassword.SequenceEqual(newPassword))
            {
                return Result.Failure(DomainErrors.Password.SamePassword);
            }
            var verify = await VerifyPasswordAsync(member,oldPassword);

            if (!verify.Value)
            {
                return Result.Failure(DomainErrors.Member.InvalidCredentials);
            }
            return await ResetPasswordAsync(member, newPassword,cancellationToken : cancellationToken);
        }

        public Result<string> HashPassword(Member member, string password)
        {
            return _hasher.HashPassword(member, password);
        }

        public async Task<Result<string>> HashResetToken(Member member,string token)
        {

            var hashedToken = token.HashToken();

           await  _passwordResetTokenRepository.RemoveByUserId(member.Id);

            var passwordResetToken = new PasswordResetToken(Guid.NewGuid(), member, hashedToken, DateTime.UtcNow.AddMinutes(5));

            _passwordResetTokenRepository.Add(passwordResetToken);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(token);
        }

        public async Task<Result> ResetPasswordAsync(Member member, string newPassword, PasswordResetToken? token = null, CancellationToken cancellationToken = default)
        {
            var passwordResult = Password.Create(newPassword);
            if (passwordResult.IsFailure)
            {
                return Result.Failure(passwordResult.Error);
            }

            var newHash = HashPassword(member, newPassword);
            member.SetPassword(newHash.Value);

            _memberRepository.Update(member);

            if(token is not null)
            {
                _passwordResetTokenRepository.Remove(token);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }

        public async Task<Result<bool>> VerifyPasswordAsync(Member member, string providedPassword,CancellationToken cancellationToken = default)
        {
            var verify = _hasher.VerifyHashedPassword(member, member.HashedPassword, providedPassword);
            if(verify == PasswordVerificationResult.Success)
            {
                return Result.Success(true);
            }
            if(verify == PasswordVerificationResult.SuccessRehashNeeded)
            {
                await ChangePasswordAsync(member,providedPassword,providedPassword,cancellationToken);
                return true;
            }

            return Result.Failure<bool>(DomainErrors.Member.InvalidCredentials);
        }

        public async Task<Result<PasswordResetToken>> VerifyTokenAsync(Member member, string token,CancellationToken cancellationToken = default)
        {
            var hashedToken = TokenService.HashToken(token);

            var storedToken = await _passwordResetTokenRepository.GetByTokenAsync(member,hashedToken, cancellationToken);

            if(storedToken is null || storedToken.Used)
            {
                return Result.Failure<PasswordResetToken>(DomainErrors.PasswordResetToken.Invalid);
            }
            if(storedToken.ExpiresAt<= DateTime.UtcNow)
            {
                return Result.Failure<PasswordResetToken>(DomainErrors.PasswordResetToken.Expired);
            }
            

            return Result.Success<PasswordResetToken>(storedToken);
        }

        
    }
}
