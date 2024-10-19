using Account.Domain.Common;

namespace Account.Domain.Entities;

public class Account : EntityBase
{
    public string Number { get; set; }
    public string Type { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal CurrentBalance { get; set; }
    public bool Status { get; set; }
    public int ClientId { get; set; }
}