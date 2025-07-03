using Application.Abstractions.Messaging;
using Domain.Repository;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.GetMemberById
{
    internal sealed class GetMemberByIdQueryHandler : IQueryHandler<GetMemberByIdQuery, MemberResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Result<MemberResponse>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId,cancellationToken);
            
            if(member is null)
            {
                return Result.Failure<MemberResponse>(new Error("Member.NotFound", $"The member with Id {request.MemberId} was not found"));
            }

            var response = new MemberResponse(member.Id, member.Email.Value);
            return response;
        }
    }
}
