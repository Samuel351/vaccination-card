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
        private readonly VaccinationService _vaccinationService;
        private readonly Guid PersonId = Guid.NewGuid();
        private readonly Guid VaccineId = Guid.NewGuid();

        public VaccinationServiceTests()
        {
            _vaccinationRepositoryMock = new Mock<IVaccinationRepository>();
            _vaccinationService = new VaccinationService(_vaccinationRepositoryMock.Object);

        }

        [Fact]
        public async Task ValidateNewDose_ShouldReturnFailure_WhenDoseAlreadyApplied()
        {

            // Arrange
            var vaccine = new Vaccine("COVID-19", 3);

            var vaccination = new Vaccination(VaccineId, PersonId, 1, DateTime.Now);

            var savedVaccinations = new List<Vaccination>()
            {
                new(VaccineId, PersonId, 1, DateTime.Now.AddDays(-90)),
                new(VaccineId, PersonId, 2, DateTime.Now.AddDays(-60)),
                new(VaccineId, PersonId, 3, DateTime.Now.AddDays(-30))
            };

            _vaccinationRepositoryMock.Setup(repo => repo.GetVaccinations(vaccination.VaccineId, vaccination.PersonId))
                .ReturnsAsync(savedVaccinations);

            // Act
            var result = await _vaccinationService.ValidateNewDose(vaccination, vaccine);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccinationErrors.DoseAlreadyApplied, result.Error);

        }

        [Fact]
        public async Task ValidateNewDose_ShouldReturnFailure_WhenDoseExceedsTheRequiredNumberOfDoses()
        {
            // Arrange
            // Vaccine requires just one dose
            var vaccine = new Vaccine("COVID-19", 1);

            var vaccination = new Vaccination(VaccineId, PersonId, 2, DateTime.Now);

            var savedVaccinations = new List<Vaccination>()
            {
                new(VaccineId, PersonId, 1, DateTime.Now.AddDays(-90)),
            };

            _vaccinationRepositoryMock.Setup(repo => repo.GetVaccinations(vaccination.VaccineId, vaccination.PersonId))
                .ReturnsAsync(savedVaccinations);

            // Act
            var result = await _vaccinationService.ValidateNewDose(vaccination, vaccine);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccinationErrors.DoseExceedsRequired, result.Error);

        }

        [Fact]
        public async Task ValidateNewDose_ShouldReturnFailure_WhenPreviousDoseHasNotBeenApplied()
        {
            // Arrange
            // Vaccine requires four doses
            var vaccine = new Vaccine("COVID-19", 4);

            var vaccination = new Vaccination(VaccineId, PersonId, 4, DateTime.Now);

            var savedVaccinations = new List<Vaccination>()
            {
                new(VaccineId, PersonId, 1, DateTime.Now.AddDays(-90)),
                new(VaccineId, PersonId, 2, DateTime.Now.AddDays(-60)),
            };

            _vaccinationRepositoryMock.Setup(repo => repo.GetVaccinations(vaccination.VaccineId, vaccination.PersonId))
                .ReturnsAsync(savedVaccinations);

            // Act
            var result = await _vaccinationService.ValidateNewDose(vaccination, vaccine);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(VaccinationErrors.DoseNotAppliedYet(3), result.Error);

        }
    }
}
