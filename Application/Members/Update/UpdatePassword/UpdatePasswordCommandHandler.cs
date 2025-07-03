using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repository;
using Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Application.Members.Update.UpdatePassword
{
    internal sealed class UpdatePasswordCommandHandler : ICommandHandler<UpdatePasswordCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdatePasswordCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork, IPasswordService passwordService,IHttpContextAccessor httpContextAccessor)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            //var user = _httpContextAccessor.HttpContext.User;
            ////var userId = user.Identities.FirstOrDefault(u => u.HasClaim(c => c.Type == ClaimTypes.NameIdentifier));
            //var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

            if(member == null)
            {
                return Result.Failure(DomainErrors.Member.NotFound(request.MemberId));
            }
            
            var result = await _passwordService.ChangePasswordAsync(member,request.OldPassword,request.NewPassword);

            return result;           
            
        }
    }
}
