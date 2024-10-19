using MediatR;

namespace Account.Application.Features.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommand : IRequest<Unit>
{
    public int Id { get; set; }
}