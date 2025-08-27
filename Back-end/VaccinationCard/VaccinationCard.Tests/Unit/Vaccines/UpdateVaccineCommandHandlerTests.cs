using Moq;
using VaccinationCard.Application.Vaccines.Commands.UpdateVaccine;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using Xunit;

namespace VaccinationCard.Tests.Unit.Vaccines
{
    public class UpdateVaccineCommandHandlerTests
    {
        private readonly Mock<IVaccineRepository> _vaccineRepositoryMock;
        private readonly Mock<IVaccinationRepository> _vaccinationRepositoryMock;
        private readonly UpdateVaccineCommandHandler _handler;

        public UpdateVaccineCommandHandlerTests()
        {
            _vaccineRepositoryMock = new Mock<IVaccineRepository>();
            _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
            _handler = new UpdateVaccineCommandHandler(_vaccineRepositoryMock.Object, _vaccinationRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenVaccineNotFound()
        {
            // Arrange
            var command = new UpdateVaccineCommand(Guid.NewGuid(), "COVID-19", 2);

            _vaccineRepositoryMock.Setup(r => r.GetByIdAsync(command.VaccineId)).ReturnsAsync((Vaccine)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccineErrors.NotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewNameAlreadyExists()
        {
            // Arrange
            var vaccineId = Guid.NewGuid();
            var existingVaccine = new Vaccine("OldName", 1);
            SetEntityId(existingVaccine, vaccineId);

            var command = new UpdateVaccineCommand(vaccineId, "NewName", 3);

            _vaccineRepositoryMock.Setup(r => r.GetByIdAsync(vaccineId)).ReturnsAsync(existingVaccine);
            _vaccineRepositoryMock.Setup(r => r.NameExists(command.Name)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccineErrors.VaccineAlreadyRegistered, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenVaccineIsBeingUsed()
        {
            // Arrange
            var vaccineId = Guid.NewGuid();
            var existingVaccine = new Vaccine("SameName", 1);
            SetEntityId(existingVaccine, vaccineId);

            var command = new UpdateVaccineCommand(vaccineId, "SameName", 3);

            _vaccineRepositoryMock.Setup(r => r.GetByIdAsync(vaccineId)).ReturnsAsync(existingVaccine);
            _vaccinationRepositoryMock.Setup(r => r.IsVaccineBeingUsed(vaccineId)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccineErrors.VaccineIsBeingUsed, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldUpdateVaccine_WhenDataIsValid()
        {
            // Arrange
            var vaccineId = Guid.NewGuid();
            var existingVaccine = new Vaccine("OldName", 1);
            SetEntityId(existingVaccine, vaccineId);

            var command = new UpdateVaccineCommand(vaccineId, "NewName", 3);

            _vaccineRepositoryMock.Setup(r => r.GetByIdAsync(vaccineId)).ReturnsAsync(existingVaccine);
            _vaccineRepositoryMock.Setup(r => r.NameExists(command.Name)).ReturnsAsync(false);
            _vaccinationRepositoryMock.Setup(r => r.IsVaccineBeingUsed(vaccineId)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        private void SetEntityId(Vaccine vaccine, Guid id)
        {
            typeof(Vaccine).GetProperty("EntityId")!.SetValue(vaccine, id);
        }
    }
}
