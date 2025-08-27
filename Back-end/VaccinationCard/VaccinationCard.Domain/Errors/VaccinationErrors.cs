using VaccinationCard.Domain.Shared;
namespace VaccinationCard.Domain.Errors
{
    public static class VaccinationErrors
    {
        public static readonly Error NotFound = new(
            "Vaccination.NotFound",
            "Sem registro de vacinação encontrado");

        public static readonly Error InvalidDose = new(
            "Vaccination.InvalidDose",
            "Dose inválida");

        public static readonly Error DoseAlreadyApplied = 
            new("Vaccination.DoseAlreadyApplied", 
                "Dose já foi aplicada");

        public static readonly Error DoseExceedsRequired =
            new("Vaccination.DoseAlreadySurpassRequired",
                "Quantidade de dose maximas já foi alcançada");

        public static readonly Error DoseApplicationDate =
            new("Vaccination.DoseApplicationDate",
                "A data de aplicação da vacina e menor que as doses anteriores");

        public static readonly Error DoseApplicationDateInFuture =
            new("Vaccination.DoseApplicationDate",
                "Data de vacinação não pode ser no futuro");

        public static readonly Error InvalidDoseNumber =
            new("Vaccination.InvalidDoseNumber",
                "Dose da vacina deve ser maior ou igual a 1");

        public static Error DoseNotAppliedYet(int doseNumber) =>
            new("Vaccination.InvalidDose", $"Dose {doseNumber} ainda não foi aplicada");
    }
}
