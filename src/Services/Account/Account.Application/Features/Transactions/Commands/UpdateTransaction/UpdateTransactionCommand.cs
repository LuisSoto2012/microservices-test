using MediatR;

namespace Account.Application.Features.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public int AccountId { get; set; }
}