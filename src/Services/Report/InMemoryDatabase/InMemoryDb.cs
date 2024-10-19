using System.Transactions;
using Report.Service.Domain.Entities;

namespace Report.Service.InMemoryDatabase;

public static class InMemoryDb
{
    public static List<TransactionInfo> Transactions { get; } = new List<TransactionInfo>();
    public static List<ClientInfo> Clients { get; } = new List<ClientInfo>();
}