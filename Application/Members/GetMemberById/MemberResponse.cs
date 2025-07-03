using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.GetMemberById
{
    public sealed record MemberResponse(Guid id,string Email);
}
