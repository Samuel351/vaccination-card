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

            var vaccinationsResponse = vaccinations
                .GroupBy(x => new { x.VaccineId, x.Vaccine.Name })
                .Select(x => new VaccinationCardResponse(x.Key.VaccineId, x.Key.Name, [.. x.Select(x => new VaccineDoseResponse(x.EntityId,x.ApplicationDate, x.DoseNumber))]))
                .ToList();

            return Result<List<VaccinationCardResponse>>.Success(vaccinationsResponse);
        }
    }
}
