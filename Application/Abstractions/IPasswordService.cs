using Domain.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IPasswordService
    {
        Result<string> HashPassword(Member member, string password);
        Task<Result<bool>> VerifyPasswordAsync(Member member, string providedPassword,CancellationToken cancellationToken = default);
        Task<Result> ChangePasswordAsync(Member member,string oldPassword,string newPassword,CancellationToken cancellationToken=default);
        Task<Result<string>> HashResetToken(Member member,string token);
        Task<Result<PasswordResetToken>> VerifyTokenAsync(Member member, string token,CancellationToken cancellationToken = default);
        Task<Result> ResetPasswordAsync(Member member, string newPassword, PasswordResetToken token, CancellationToken cancellationToken = default);

    }
}
