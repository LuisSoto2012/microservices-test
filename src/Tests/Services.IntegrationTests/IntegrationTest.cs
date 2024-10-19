using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Text.Json;

namespace Services.IntegrationTests;

public class IntegrationTest : IClassFixture<WebApplicationFactory<Client.API.Program>>, 
    IClassFixture<WebApplicationFactory<Account.API.Program>>, 
    IClassFixture<WebApplicationFactory<Report.Service.Program>>
{
    private readonly HttpClient _clientServiceClient;
    private readonly HttpClient _accountServiceClient;
    private readonly HttpClient _reportServiceClient;

    public IntegrationTest(WebApplicationFactory<Client.API.Program> clientFactory,
                                  WebApplicationFactory<Account.API.Program> accountFactory,
                                  WebApplicationFactory<Report.Service.Program> reportFactory)
    {
        _clientServiceClient = clientFactory.CreateClient();
        _accountServiceClient = accountFactory.CreateClient();
        _reportServiceClient = reportFactory.CreateClient();
    }

    [Fact]
    public async Task GenerateReport_ShouldReturnReport_WhenClientAndTransactionCreated()
    {
        // 1. Create client in Client Service
        var newClient = new
        {
            Name = "Cliente Prueba",
            Gender = "Male",
            Age = 30,
            IdentificationNumber = "123456789",
            Address = "123 Calle Falsa",
            Phone = "123-456-7890",
            Password = "password123"
        };
        var clientResponse = await _clientServiceClient.PostAsJsonAsync("/api/v1/clients", newClient);
        clientResponse.EnsureSuccessStatusCode();
        var clientContent = await clientResponse.Content.ReadAsStringAsync();
        var clientId = JsonSerializer.Deserialize<ClientResponse>(clientContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })?.Id;

        // 2. Create account in Account Service
        var newAccount = new { ClientId = clientId, Type = "Corriente", Number = "01010101", InitialBalance = 1000.00m, Status = true };
        var accountResponse = await _accountServiceClient.PostAsJsonAsync("/api/v1/accounts", newAccount);
        accountResponse.EnsureSuccessStatusCode();
        var accountContent = await accountResponse.Content.ReadAsStringAsync();
        var accountId = JsonSerializer.Deserialize<AccountResponse>(accountContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })?.Id;

        // 3. Create transaction in Account Service
        var newTransaction = new { AccountId = accountId, Amount = 100.0, Type = "Deposito" };
        var transactionResponse = await _accountServiceClient.PostAsJsonAsync("/api/v1/transactions", newTransaction);
        transactionResponse.EnsureSuccessStatusCode();

        // 4. Generate report in Report Service
        var startDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
        var endDate = DateTime.Now.ToString("yyyy-MM-dd");
        var reportResponse = await _reportServiceClient.GetAsync($"/api/v1/reports/client-transactions?clientId={clientId}&startDate={startDate}&endDate={endDate}");
        reportResponse.EnsureSuccessStatusCode();
        
        // 5. Validate report
        var reportContent = await reportResponse.Content.ReadAsStringAsync();
        var report = JsonSerializer.Deserialize<List<ReportResponse>>(reportContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(report);
        Assert.NotEmpty(report); 
    }
}

public class ClientResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class AccountResponse
{
    public int Id { get; set; }
    public string Number { get; set; }
}

public class ReportResponse
{
    public string ClientName { get; set; }
    public DateTime Date { get; set; }
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
}