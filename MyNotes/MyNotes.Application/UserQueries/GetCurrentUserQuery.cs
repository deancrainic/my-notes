using MediatR;
using MyNotes.Domain.Entities;

namespace MyNotes.Application.UserQueries;

public class GetCurrentUserQuery : IRequest<User>
{
    public string Token { get; set; }
}