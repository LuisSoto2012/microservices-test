using MediatR;

namespace Account.Application.Features.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommand : IRequest<Unit>
{
    public int Id { get; set; }
}