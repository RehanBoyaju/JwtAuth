using Application.Abstractions.Messaging;

namespace Application.Members.Update.UpdateMember;

public sealed record UpdateMemberRequest(string Email, string FirstName, string LastName) : ICommand;
