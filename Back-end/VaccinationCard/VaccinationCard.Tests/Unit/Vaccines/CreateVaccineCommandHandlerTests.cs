using Moq;
using VaccinationCard.Application.Vaccines.Commands.CreateVaccine;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using Xunit;

namespace VaccinationCard.Tests.Unit.Vaccines
{
    public class CreateVaccineCommandHandlerTests
    {
        private readonly Mock<IVaccineRepository> _vaccineRepositoryMock;
        private readonly CreateVaccineCommandHandler _handler;

        public CreateVaccineCommandHandlerTests()
        {
            _vaccineRepositoryMock = new Mock<IVaccineRepository>();
            _handler = new CreateVaccineCommandHandler(_vaccineRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenVaccineNameAlreadyExists()
        {
            // Arrange
            var command = new CreateVaccineCommand("COVID-19", 2);

            _vaccineRepositoryMock
                .Setup(r => r.NameExists(command.Name))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccineErrors.VaccineAlreadyRegistered, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldCreateVaccine_WhenNameIsUnique()
        {
            // Arrange
            var command = new CreateVaccineCommand("COVID-19", 2);

            _vaccineRepositoryMock
                .Setup(r => r.NameExists(command.Name))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
