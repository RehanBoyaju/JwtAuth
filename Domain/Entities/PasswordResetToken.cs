using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PasswordResetToken : Entity
    {
        public Guid MemberId { get; init; }
        public Member Member { get; init; }
        public string TokenHash { get; init; }
        public DateTime ExpiresAt { get; init; }
        public bool Used { get; private set; }

        public PasswordResetToken(Guid id,Member member, string tokenHash, DateTime expiresAt) : base(id)
        {
            Member = member;
            MemberId = member.Id;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
            Used = false;
        }
        public PasswordResetToken()
        {

        }
        public void MarkAsUsed()
        {
            Used = true;
        }
    }
}
