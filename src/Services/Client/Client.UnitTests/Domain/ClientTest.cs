using Client.Domain.Entities;
using Xunit;

namespace Client.UnitTests.Domain;

public class ClientTest
{
    [Fact]
    public void Create_Client_Success()
    {
        // Act
        var client = new Client.Domain.Entities.Client
        {
            Name = "Luis Soto",
            Gender = "Male",
            Age = 30,
            IdentificationNumber = "123456789",
            Address = "123 Main Street",
            Phone = "555-5555",
            Password = "password123",
            Status = true,
            CreatedBy = "admin",
            CreatedDate = DateTime.Now
        };

        // Assert
        Assert.NotNull(client);
    }
    
    [Fact]
    public void Client_InheritsFrom_Person()
    {
        // Arrange & Act
        var client = new Client.Domain.Entities.Client();

        // Assert
        Assert.IsAssignableFrom<Person>(client);
    }
}