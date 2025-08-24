using Domain.Abstractions;

namespace VaccinationCard.Domain.Errors
{
    public static class PersonErrors
    {
        public static readonly Error NotFound = new(
            "Person.NotFound",
            "This person was not found");

    }
}
