namespace Account.Application.Features.Accounts.Queries.GetAccountsList;

public class AccountsVm
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string Type { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal CurrentBalance { get; set; }
    public bool Status { get; set; }
    public int ClientId { get; set; }
}