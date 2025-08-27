using Moq;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Services;
using Xunit;

namespace VaccinationCard.Tests.Vaccinations
{
    public class VaccinationServiceTests
    {

        private readonly Mock<IVaccinationRepository> _vaccinationRepositoryMock;
        private readonly VaccinationService _service;

        public VaccinationServiceTests()
        {
            _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
            _service = new VaccinationService(_vaccinationRepositoryMock.Object);
        }


        [Fact]
        public async Task ValidateNewDose_ShouldReturnFailure_WhenDoseAlreadyApplied()
        {

            // Arrange
            var vaccine = new Vaccine("COVID-19", 3);
            var vaccineId = Guid.NewGuid();

            var personId = Guid.NewGuid();

            var vaccination = new Vaccination(vaccineId, personId, 1, DateTime.Now);

            var savedVaccinations = new List<Vaccination>()
            {
                new(vaccineId, personId, 1, DateTime.Now.AddDays(-90)),
                new(vaccineId, personId, 2, DateTime.Now.AddDays(-60)),
                new(vaccineId, personId, 3, DateTime.Now.AddDays(-30))
            };

            _vaccinationRepositoryMock.Setup(repo => repo.GetVaccinations(vaccination.VaccineId, vaccination.PersonId))
                .ReturnsAsync(savedVaccinations);

            // Act
            var result = await _service.ValidateNewDose(vaccination, vaccine);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccinationErrors.DoseAlreadyApplied, result.Error);

        }
    }
}
