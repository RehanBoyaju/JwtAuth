using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repository;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Members.Delete
{
    public class DeleteCommandHandler : ICommandHandler<DeleteCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommandHandler(IMemberRepository memberRepository,IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            Member? member = await _memberRepository.GetByIdAsync(request.memberId,cancellationToken);
            if(member is null)
            {
                return Result.Failure(DomainErrors.Member.NotFound(request.memberId));
            }

            _memberRepository.Delete(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
