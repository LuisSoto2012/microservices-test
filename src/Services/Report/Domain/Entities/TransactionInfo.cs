namespace Report.Service.Domain.Entities;

public class TransactionInfo
{
    public int ClientId { get; set; }
    public DateTime Date { get; set; }
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
}