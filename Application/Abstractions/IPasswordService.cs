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
        string HashPassword(Member member, string password);
        Task<bool> VerifyPasswordAsync(Member member, string providedPassword);
        Task<Result> ChangePasswordAsync(Member member,string oldPassword,string newPassword,CancellationToken cancellationToken=default);
    }
}
