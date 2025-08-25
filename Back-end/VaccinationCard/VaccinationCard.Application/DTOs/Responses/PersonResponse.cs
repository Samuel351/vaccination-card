namespace VaccinationCard.Application.DTOs.Responses
{
    public sealed record PersonResponse(Guid PersonId, string Name, string CPF, string Email, string PhoneNumber, string Gender, int Age);
}
