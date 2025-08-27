using Domain.Abstractions;
using Moq;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Application.Vaccinations.Commands.CreateVaccination;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Services;
using Xunit;

namespace VaccinationCard.Tests.Unit.Vaccinations
{
    public class CreateVaccinationCommandHandlerTests
    {
        private readonly Mock<VaccinationService> _vaccinationServiceMock;
        private readonly Mock<IVaccinationRepository> _vaccinationRepositoryMock;
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<IBaseRepository<Vaccine>> _vaccineRepositoryMock;
        private readonly CreateVaccinationCommandHandler _handler;

        public CreateVaccinationCommandHandlerTests()
        {
            _vaccinationServiceMock = new Mock<VaccinationService>(null);
            _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
            _personRepositoryMock = new Mock<IPersonRepository>();
            _vaccineRepositoryMock = new Mock<IBaseRepository<Vaccine>>();

            _handler = new CreateVaccinationCommandHandler(
                _vaccinationServiceMock.Object,
                _vaccinationRepositoryMock.Object,
                _personRepositoryMock.Object,
                _vaccineRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenPersonNotFound()
        {
            // Arrange
            var command = new CreateVaccinationCommand(Guid.NewGuid(), Guid.NewGuid(), 1, DateTime.UtcNow);

            _personRepositoryMock
                .Setup(r => r.GetByIdAsync(command.PersonId))
                .ReturnsAsync((Person?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(PersonErrors.NotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenVaccineNotFound()
        {
            // Arrange
            var person = new Person
            {
                EntityId = Guid.NewGuid()
            };
            var command = new CreateVaccinationCommand(person.EntityId, Guid.NewGuid(), 1, DateTime.UtcNow);

            _personRepositoryMock
                .Setup(r => r.GetByIdAsync(command.PersonId))
                .ReturnsAsync(person);
            _vaccineRepositoryMock
                .Setup(r => r.GetByIdAsync(command.VaccineId))
                .ReturnsAsync((Vaccine?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccineErrors.NotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldNotCallAdd_WhenPersonNotFound()
        {
            // Arrange
            var command = new CreateVaccinationCommand(Guid.NewGuid(), Guid.NewGuid(), 1, DateTime.UtcNow);

            _personRepositoryMock
                .Setup(r => r.GetByIdAsync(command.PersonId))
                .ReturnsAsync((Person?)null);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _vaccinationRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Vaccination>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldNotCallAdd_WhenVaccineNotFound()
        {
            // Arrange
            var person = new Person
            {
                EntityId = Guid.NewGuid()
            };
            var command = new CreateVaccinationCommand(person.EntityId, Guid.NewGuid(), 1, DateTime.UtcNow);

            _personRepositoryMock
                .Setup(r => r.GetByIdAsync(command.PersonId))
                .ReturnsAsync(person);
            _vaccineRepositoryMock
                .Setup(r => r.GetByIdAsync(command.VaccineId))
                .ReturnsAsync((Vaccine?)null);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _vaccinationRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Vaccination>()), Times.Never);
        }
    }
}