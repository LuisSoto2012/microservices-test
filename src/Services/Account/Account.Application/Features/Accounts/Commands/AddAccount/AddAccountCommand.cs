using MediatR;

namespace Account.Application.Features.Accounts.Commands.AddAccount;

public class AddAccountCommand : IRequest<Domain.Entities.Account>
{
    public string Number { get; set; }
    public string Type { get; set; }
    public decimal InitialBalance { get; set; }
    public bool Status { get; set; }
    public int ClientId { get; set; }
}