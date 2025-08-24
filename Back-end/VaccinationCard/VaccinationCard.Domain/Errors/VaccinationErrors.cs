using Domain.Abstractions;
namespace VaccinationCard.Domain.Errors
{
    public static class VaccinationErrors
    {
        public static readonly Error NotFound = new(
            "Vaccination.NotFound",
            "No vaccinations were found");
    }
}
