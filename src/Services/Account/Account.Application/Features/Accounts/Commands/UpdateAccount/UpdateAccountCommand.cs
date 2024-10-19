using MediatR;

namespace Account.Application.Features.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string Type { get; set; }
    public decimal InitialBalance { get; set; }
    public bool Status { get; set; }
    public int ClientId { get; set; }
}