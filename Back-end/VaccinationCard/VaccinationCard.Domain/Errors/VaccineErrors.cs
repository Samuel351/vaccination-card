using Domain.Abstractions;

namespace VaccinationCard.Domain.Errors
{
    public static class VaccineErrors
    {
        public static readonly Error NotFound = new(
            "Vaccines.NotFound",
            "Vacina não encontrada");

        public static readonly Error VaccineIsBeingUsed = new(
            "Vaccines.BeingUsed",
            "Essa vacina está sendo utilizada");
    }
}
