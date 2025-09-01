using Moq;
using VaccinationCard.Application.Vaccines.Commands.DeleteVaccine;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Application.Interfaces.Repositories;
using Xunit;

namespace VaccinationCard.Tests.Unit.Vaccines
{
    public class DeleteVaccineCommandHandlerTests
    {
        private readonly Mock<IBaseRepository<Vaccine>> _vaccineRepositoryMock;
        private readonly Mock<IVaccinationRepository> _vaccinationRepositoryMock;
        private readonly DeleteVaccineCommandHandler _handler;

        public DeleteVaccineCommandHandlerTests()
        {
            _vaccineRepositoryMock = new Mock<IBaseRepository<Vaccine>>();
            _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
            _handler = new DeleteVaccineCommandHandler(_vaccineRepositoryMock.Object, _vaccinationRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenVaccineNotFound()
        {
            // Arrange
            var command = new DeleteVaccineCommand(Guid.NewGuid());
            _vaccineRepositoryMock.Setup(r => r.GetByIdAsync(command.VaccineId, CancellationToken.None))
                .ReturnsAsync((Vaccine)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccineErrors.NotFound, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenVaccineIsBeingUsed()
        {
            // Arrange
            var vaccineId = Guid.NewGuid();
            var existingVaccine = new Vaccine("COVID-19", 2);
            existingVaccine.EntityId = Guid.NewGuid();

            var command = new DeleteVaccineCommand(vaccineId);

            _vaccineRepositoryMock.Setup(r => r.GetByIdAsync(vaccineId, CancellationToken.None)).ReturnsAsync(existingVaccine);
            _vaccinationRepositoryMock.Setup(r => r.IsVaccineBeingUsed(vaccineId, CancellationToken.None)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccineErrors.VaccineIsBeingUsed, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldDeleteVaccine_WhenVaccineExistsAndIsNotBeingUsed()
        {
            // Arrange
            var vaccineId = Guid.NewGuid();
            var existingVaccine = new Vaccine("COVID-19", 2);
            existingVaccine.EntityId = Guid.NewGuid();

            var command = new DeleteVaccineCommand(vaccineId);

            _vaccineRepositoryMock.Setup(r => r.GetByIdAsync(vaccineId, CancellationToken.None)).ReturnsAsync(existingVaccine);
            _vaccinationRepositoryMock.Setup(r => r.IsVaccineBeingUsed(vaccineId, CancellationToken.None)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
