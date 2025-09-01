using Moq;
using VaccinationCard.Application.Persons.Commands.UpdatePerson;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using Xunit;

namespace VaccinationCard.Tests.Unit.Persons
{
    public class UpdatePersonCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly UpdatePersonCommandHandler _handler;

        public UpdatePersonCommandHandlerTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _handler = new UpdatePersonCommandHandler(_personRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenPersonNotFound()
        {
            // Arrange
            var command = new UpdatePersonCommand(
                Guid.NewGuid(), "John Doe", "12345678900",
                "john@email.com", "11999999999", "M", 30);

            _personRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId, CancellationToken.None))
                .ReturnsAsync((Person)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(PersonErrors.NotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCPFAlreadyBelongsToAnotherPerson()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var command = new UpdatePersonCommand(
                personId, "John Doe", "12345678900",
                "john@email.com", "11999999999", "M", 30);

            var existingPerson = new Person("Old Name", "98765432100", "old@email.com", "11888888888", "M", 40);
            existingPerson.EntityId = personId;

            var cpfOwner = new Person("Another Person", "12345678900", "another@email.com", "11777777777", "F", 25);
            cpfOwner.EntityId = Guid.NewGuid();

            _personRepositoryMock.Setup(r => r.GetByIdAsync(personId, CancellationToken.None)).ReturnsAsync(existingPerson);
            _personRepositoryMock.Setup(r => r.GetPersonByCPF(command.CPF, CancellationToken.None)).ReturnsAsync(cpfOwner);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(PersonErrors.CPFAlreadyExists, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailAlreadyBelongsToAnotherPerson()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var command = new UpdatePersonCommand(
                personId, "John Doe", "12345678900",
                "john@email.com", "11999999999", "M", 30);

            var existingPerson = new Person("Old Name", "98765432100", "old@email.com", "11888888888", "M", 40);
            existingPerson.EntityId = personId;

            var emailOwner = new Person("Another Person", "11122233344", "john@email.com", "11777777777", "F", 25);
            emailOwner.EntityId = Guid.NewGuid();

            _personRepositoryMock.Setup(r => r.GetByIdAsync(personId, CancellationToken.None)).ReturnsAsync(existingPerson);
            _personRepositoryMock.Setup(r => r.GetPersonByCPF(command.CPF, CancellationToken.None)).ReturnsAsync((Person)null);
            _personRepositoryMock.Setup(r => r.GetPersonByEmail(command.Email, CancellationToken.None)).ReturnsAsync(emailOwner);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(PersonErrors.EmailAlreadyExists, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldUpdatePerson_WhenDataIsValid()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var command = new UpdatePersonCommand(
                personId, "John Doe", "12345678900",
                "john@email.com", "11999999999", "M", 30);

            var existingPerson = new Person("Old Name", "98765432100", "old@email.com", "11888888888", "M", 40);
            existingPerson.EntityId = personId;

            _personRepositoryMock.Setup(r => r.GetByIdAsync(personId, CancellationToken.None)).ReturnsAsync(existingPerson);
            _personRepositoryMock.Setup(r => r.GetPersonByCPF(command.CPF, CancellationToken.None)).ReturnsAsync((Person)null);
            _personRepositoryMock.Setup(r => r.GetPersonByEmail(command.Email, CancellationToken.None)).ReturnsAsync((Person)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
