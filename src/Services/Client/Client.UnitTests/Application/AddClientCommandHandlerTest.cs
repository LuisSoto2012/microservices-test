using AutoMapper;
using Client.Application.Contracts;
using Client.Application.Features.Clients.Commands.AddClient;
using Client.Application.IntegrationEvents.Events;
using EventBus.Abstractions;
using Moq;
using Xunit;

namespace Client.UnitTests.Application;

public class AddClientCommandHandlerTest
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly AddClientCommandHandler _handler;

    public AddClientCommandHandlerTest()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _mapperMock = new Mock<IMapper>();
        _eventBusMock = new Mock<IEventBus>();
        _handler = new AddClientCommandHandler(_clientRepositoryMock.Object, _mapperMock.Object, _eventBusMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_AddsClientAndPublishesEvent()
    {
        // Arrange
        var command = new AddClientCommand
        {
            Name = "Luis Soto",
            Gender = "Male",
            Age = 30,
            IdentificationNumber = "123456789",
            Address = "123 Main Street",
            Phone = "555-5555",
            Password = "password123"
        };

        var clientEntity = new Client.Domain.Entities.Client
        {
            Id = 1,
            Name = command.Name,
            Gender = command.Gender,
            Age = command.Age,
            IdentificationNumber = command.IdentificationNumber,
            Address = command.Address,
            Phone = command.Phone,
            Password = command.Password
        };

        _mapperMock.Setup(m => m.Map<Client.Domain.Entities.Client>(command)).Returns(clientEntity);
        _clientRepositoryMock.Setup(r => r.AddAsync(clientEntity)).ReturnsAsync(clientEntity);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _clientRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Client.Domain.Entities.Client>()), Times.Once);
        _eventBusMock.Verify(e => e.PublishAsync(It.IsAny<ClientCreatedEvent>()), Times.Once);
    }
}