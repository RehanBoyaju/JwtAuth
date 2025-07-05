using Application.Abstractions.Messaging;

namespace Application.Members.Update.UpdateMember;

public sealed record UpdateMemberCommand(Guid MemberId,string Email, string FirstName, string LastName) : ICommand;
