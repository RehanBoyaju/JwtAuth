using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IPasswordResetTokenRepository
    {
        void Add(PasswordResetToken token);
        Task<PasswordResetToken?> GetByTokenAsync(Member member,string token, CancellationToken cancellationToken);
        void Remove(PasswordResetToken token);
        Task RemoveByUserId(Guid userId, CancellationToken cancellationToken=default);
    }
}
