using Domain.Abstractions;
namespace VaccinationCard.Domain.Errors
{
    public static class VaccinationErrors
    {
        public static readonly Error NotFound = new(
            "Vaccination.NotFound",
            "No vaccinations were found");

        public static readonly Error InvalidDose = new(
            "Vaccination.InvalidDose",
            "Invalid dose");

        public static readonly Error DoseAlreadyApplied = 
            new("Vaccination.DoseAlreadyApplied", 
                "Dose já foi aplicada");

        public static readonly Error DoseAlreadySurpassRequired =
            new("Vaccination.DoseAlreadySurpassRequired",
                "Quantidade de dose maximas já foi alcançada");

        public static Error DoseNotAppliedYet(int doseNumber) =>
            new("Vaccination.InvalidDose", $"Dose {doseNumber} ainda não foi aplicada");
    }
}
