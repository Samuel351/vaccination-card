using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;

namespace VaccinationCard.Application.Persons.Queries.GetPersonVaccinationCard
{
    public sealed record GetPersonVaccinationCardQueryHandler(IPersonRepository personRepository) : IRequestHandler<GetPersonVaccinationCardQuery, Result<List<VaccinationCardResponse>>>
    {

        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<Result<List<VaccinationCardResponse>>> Handle(GetPersonVaccinationCardQuery request, CancellationToken cancellationToken)
        {
            var vaccinations = await _personRepository.GetPersonVaccinations(request.PersonId) ?? [];

            if (vaccinations.Count == 0) return Result<List<VaccinationCardResponse>>.Failure(VaccinationErrors.NotFound, HttpStatusCode.NoContent);

            return Result<List<VaccinationCardResponse>>.Success([.. vaccinations.Select(vaccinationRecord => new VaccinationCardResponse(vaccinationRecord.EntityId, vaccinationRecord.VaccineId, vaccinationRecord.Vaccine.Name, vaccinationRecord.ApplicationDate, vaccinationRecord.DoseNumber))]);
        }
    }
}
