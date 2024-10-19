namespace Account.Application.Features.Transactions.Queries.GetTransactionsList;

public class TransactionsVm
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public int AccountId { get; set; }
}