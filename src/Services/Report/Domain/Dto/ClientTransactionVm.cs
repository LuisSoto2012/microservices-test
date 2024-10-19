namespace Report.Service.Domain.Dto;

public class ClientTransactionVm
{
    public string ClientName { get; set; }
    public DateTime Date { get; set; }
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
}