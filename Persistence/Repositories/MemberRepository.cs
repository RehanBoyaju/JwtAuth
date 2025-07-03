using Domain.Entities;
using Domain.Repository;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public sealed class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _dbContext
                .Set<Member>()
                .FirstOrDefaultAsync(member => member.Id == id, cancellationToken);

        public async Task<Member?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default) =>
            await _dbContext
                .Set<Member>()
                .FirstOrDefaultAsync(member => member.Email == email, cancellationToken);

        public async Task<bool> IsEmailUniqueAsync(
            Email email,
            CancellationToken cancellationToken = default) =>
            !await _dbContext
                .Set<Member>()
                .AnyAsync(member => member.Email == email, cancellationToken);

        public void Add(Member member) =>
            _dbContext.Set<Member>().Add(member);

        public void Update(Member member) =>
            _dbContext.Set<Member>().Update(member);


        public void Delete(Member member) => 
            _dbContext.Set<Member>().Remove(member);

    }
}
