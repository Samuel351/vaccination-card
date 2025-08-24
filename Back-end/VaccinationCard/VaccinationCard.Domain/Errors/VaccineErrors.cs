using Domain.Abstractions;

namespace VaccinationCard.Domain.Errors
{
    public static class VaccineErrors
    {
        public static readonly Error NotFound = new(
            "Vaccines.NotFound",
            "No vaccines found");
    }
}
