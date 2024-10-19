using Account.Domain.Common;

namespace Account.Domain.Entities;

public class Transaction : EntityBase
{
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }
}