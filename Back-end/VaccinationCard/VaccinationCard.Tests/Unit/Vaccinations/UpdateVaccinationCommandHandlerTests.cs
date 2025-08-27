using Domain.Abstractions;
using Moq;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Application.Vaccinations.Commands.UpdateVaccination;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Services;
using Xunit;

namespace VaccinationCard.Tests.Unit.Vaccinations
{
    public class UpdateVaccinationCommandHandlerTests
    {
        private readonly Mock<IVaccinationRepository> _vaccinationRepositoryMock;
        private readonly Mock<IVaccineRepository> _vaccineRepositoryMock;
        private readonly VaccinationService _vaccinationServiceMock;
        private readonly UpdateVaccinationCommandHandler _handler;

        public UpdateVaccinationCommandHandlerTests()
        {
            _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
            _vaccineRepositoryMock = new Mock<IVaccineRepository>();

            _vaccinationServiceMock = new VaccinationService(_vaccinationRepositoryMock.Object);

            _handler = new UpdateVaccinationCommandHandler(
                _vaccinationRepositoryMock.Object,
                _vaccineRepositoryMock.Object,
                _vaccinationServiceMock
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenVaccinationNotFound()
        {
            // Arrange
            var command = new UpdateVaccinationCommand(Guid.NewGuid(), 2, DateTime.UtcNow);

            _vaccinationRepositoryMock
                .Setup(r => r.GetByIdAsync(command.VaccinationId))
                .ReturnsAsync((Vaccination?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccinationErrors.NotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenVaccineNotFound()
        {
            // Arrange
            var vaccination = new Vaccination(Guid.NewGuid(), Guid.NewGuid(), 1, DateTime.UtcNow)
            {
                EntityId = Guid.NewGuid()
            };

            var command = new UpdateVaccinationCommand(vaccination.EntityId, 2, DateTime.UtcNow);

            _vaccinationRepositoryMock
                .Setup(r => r.GetByIdAsync(command.VaccinationId))
                .ReturnsAsync(vaccination);
            _vaccineRepositoryMock
                .Setup(r => r.GetByIdAsync(vaccination.VaccineId))
                .ReturnsAsync((Vaccine?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccineErrors.NotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldNotCallUpdate_WhenVaccinationNotFound()
        {
            // Arrange
            var command = new UpdateVaccinationCommand(Guid.NewGuid(), 2, DateTime.UtcNow);

            _vaccinationRepositoryMock
                .Setup(r => r.GetByIdAsync(command.VaccinationId))
                .ReturnsAsync((Vaccination?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _vaccinationRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Vaccination>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldNotCallUpdate_WhenVaccineNotFound()
        {
            // Arrange
            var vaccination = new Vaccination(Guid.NewGuid(), Guid.NewGuid(), 1, DateTime.UtcNow)
            {
                EntityId = Guid.NewGuid()
            };

            var command = new UpdateVaccinationCommand(vaccination.EntityId, 2, DateTime.UtcNow);

            _vaccinationRepositoryMock
                .Setup(r => r.GetByIdAsync(command.VaccinationId))
                .ReturnsAsync(vaccination); 

            _vaccineRepositoryMock
                .Setup(r => r.GetByIdAsync(vaccination.VaccineId))
                .ReturnsAsync((Vaccine?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
        }
    }
}