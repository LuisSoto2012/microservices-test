using MediatR;

namespace Client.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommand : IRequest<Unit>
{
    public int Id { get; set; }
}