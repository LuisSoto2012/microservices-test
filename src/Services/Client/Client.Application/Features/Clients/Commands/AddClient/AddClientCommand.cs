using MediatR;

namespace Client.Application.Features.Clients.Commands.AddClient;

public class AddClientCommand : IRequest
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public string IdentificationNumber { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }   
    public string Password { get; set; }
}