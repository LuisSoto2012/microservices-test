using EventBus.Events;

namespace Report.Service.Domain.Events;

public class TransactionCreatedEvent : IntegrationEvent
{
    public int ClientId { get; set; }
    public DateTime Date { get; set; }
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    
    public TransactionCreatedEvent(int clientId, DateTime date, string accountNumber, string accountType, decimal initialBalance, decimal amount, decimal balance)
    {
        ClientId = clientId;
        Date = date;
        AccountNumber = accountNumber;
        AccountType = accountType;
        InitialBalance = initialBalance;
        Amount = amount;
        Balance = balance;
    }
}