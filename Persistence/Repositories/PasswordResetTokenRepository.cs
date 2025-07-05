using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PasswordResetTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public void Add(PasswordResetToken token)
        {
            _dbContext.Set<PasswordResetToken>().Add(token);
        }

        public async Task<PasswordResetToken?> GetByTokenAsync(Member member, string hashedToken, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<PasswordResetToken>()
                .FirstOrDefaultAsync(t => t.MemberId == member.Id && t.TokenHash == hashedToken,cancellationToken);
        }


        public void Remove(PasswordResetToken token)
        {
            _dbContext.Set<PasswordResetToken>().Remove(token);
        }

        public async Task RemoveByUserId(Guid userId, CancellationToken cancellationToken)
        {
            await _dbContext.Set<PasswordResetToken>().Where(t => t.MemberId == userId).ExecuteDeleteAsync();
        }
    }
}
