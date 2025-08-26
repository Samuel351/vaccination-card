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

        public static readonly Error NameIsToLong = new(
            "Vaccines.NameIsToLong",
            "Nome muito longo");

        public static readonly Error NameIsObligatory = new(
            "Vaccines.NameIsObligatory",
            "Nome da vacina é obrigatório");

        public static readonly Error InvalidRequiredDoses = new(
            "Vaccines.InvalidRequiredDoses",
            "A vacina precisa pelo menos ter uma dose");
    }
}
