using MediatR;

namespace Account.Application.Features.Transactions.Commands.AddTransaction;

public class AddTransactionCommand : IRequest
{
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public int AccountId { get; set; }
}