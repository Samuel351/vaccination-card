namespace VaccinationCard.Domain.Errors
{
    public static class PersonErrors
    {
        public static string PersonNotFound(Guid personId) =>
            $"Person with '{personId}' not found.";
    }
}
