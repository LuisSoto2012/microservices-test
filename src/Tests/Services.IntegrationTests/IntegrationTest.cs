using System.Net.Http.Json;
using Xunit;

namespace Services.IntegrationTests;

public class IntegrationTest
{
    private readonly HttpClient _client;

    public IntegrationTest()
    {
        var factory = new WebApplicationFactory<Startup>() // Asumiendo que usas ASP.NET Core
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Development");
            });

        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GenerateReport_ShouldReturnReport_WhenClientAndTransactionCreated()
    {
        // 1. Crear un nuevo cliente
        var clientResponse = await _client.PostAsJsonAsync("/api/client", new { /* datos del cliente */ });
        clientResponse.EnsureSuccessStatusCode();
        var clientId = await clientResponse.Content.ReadAsStringAsync();

        // 2. Crear una nueva cuenta
        var accountResponse = await _client.PostAsJsonAsync("/api/account", new { /* datos de la cuenta */ });
        accountResponse.EnsureSuccessStatusCode();

        // 3. Crear una nueva transacción
        var transactionResponse = await _client.PostAsJsonAsync("/api/account/transactions", new { /* datos de la transacción */ });
        transactionResponse.EnsureSuccessStatusCode();

        // 4. Generar el reporte
        var reportResponse = await _client.GetAsync($"/api/report?clientId={clientId}&startDate={startDate}&endDate={endDate}");
        reportResponse.EnsureSuccessStatusCode();
        
        // 5. Validar el contenido del reporte
        var reportContent = await reportResponse.Content.ReadAsStringAsync();
        Assert.NotNull(reportContent);
        // Aquí puedes agregar más validaciones según tu caso.
    }
}