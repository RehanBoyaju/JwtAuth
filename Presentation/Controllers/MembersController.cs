using Application.Members.Delete;
using Application.Members.GetMemberById;
using Application.Members.Login;
using Application.Members.Register;
using Application.Members.Update.UpdatePassword;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
 
    [Route("api/[controller]")]
    public sealed class MembersController:ApiController
    {
        
        public MembersController(ISender sender) :base(sender){
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterMemberAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(request.Email,request.FirstName,request.LastName,request.Password);


            Result<Guid> result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            return CreatedAtAction(nameof(GetMemberById), new {id = result.Value}, result.Value);         

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginMemberAsync([FromBody] LoginRequest request,CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Email,request.Password);


            Result<string> tokenResult = await Sender.Send(command,cancellationToken);

            
            if(tokenResult.IsFailure)
            {
                return HandleFailure(tokenResult);
            }

            return Ok(tokenResult.Value);

        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMemberById(Guid id,CancellationToken cancellationToken)
        {
            var query = new GetMemberByIdQuery(id);

            Result<MemberResponse> response = await Sender.Send(query,cancellationToken);
            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordRequest request,CancellationToken cancellationToken)
        {
            var memberId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                return Unauthorized();
            }
            var command = new UpdatePasswordCommand(new Guid(memberId),request.OldPassword,request.NewPassword);

            var response = await Sender.Send(command, cancellationToken);

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }

            return Ok("Password Updated Successfully");

            
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id,CancellationToken cancellationToken)
        {
            var command = new DeleteCommand(id);

            Result response = await Sender.Send(command,cancellationToken);

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }
            return NoContent();
        }
    }
}
