using Moq;
using VaccinationCard.Application.Persons.Commands.CreatePerson;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using Xunit;

namespace VaccinationCard.Tests.Persons
{
    public class CreatePersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly CreatePersonCommandHandler _handler;

        public CreatePersonCommandHandlerTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _handler = new CreatePersonCommandHandler(_personRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCPFAlreadyExists()
        {
            // Arrange
            var command = new CreatePersonCommand(
                "John Doe", "12345678900", "john@email.com", "11999999999", "M", 30);

            _personRepositoryMock.Setup(r => r.CPFExists(command.CPF)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(PersonErrors.CPFAlreadyExists, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailAlreadyExists()
        {
            // Arrange
            var command = new CreatePersonCommand(
                "John Doe", "12345678900", "john@email.com", "11999999999", "M", 30);

            _personRepositoryMock.Setup(r => r.CPFExists(command.CPF)).ReturnsAsync(false);
            _personRepositoryMock.Setup(r => r.EmailExists(command.Email)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(PersonErrors.EmailAlreadyExists, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldCreatePerson_WhenCPFAndEmailAreUnique()
        {
            // Arrange
            var command = new CreatePersonCommand(
                "John Doe", "12345678900", "john@email.com", "11999999999", "M", 30);

            _personRepositoryMock.Setup(r => r.CPFExists(command.CPF)).ReturnsAsync(false);
            _personRepositoryMock.Setup(r => r.EmailExists(command.Email)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
