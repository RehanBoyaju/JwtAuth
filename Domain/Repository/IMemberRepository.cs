using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
        Task<Member?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

        void Add(Member member);

        void Update(Member member);

        void Delete(Member member);

    }
}
