using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Persons.Queries.GetPersonVaccinationCard
{
    public sealed record GetPersonVaccinationCardQueryHandler(IPersonRepository personRepository) : IRequestHandler<GetPersonVaccinationCardQuery, Result<List<VaccinationCardResponse>>>
    {

        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<Result<List<VaccinationCardResponse>>> Handle(GetPersonVaccinationCardQuery request, CancellationToken cancellationToken)
        {
            var vaccinationRecords = await _personRepository.GetPersonVaccinationRecords(request.PersonId) ?? [];

            if (vaccinationRecords.Count == 0) return Result<List<VaccinationCardResponse>>.Failure(VaccinationRecordErrors.NoVaccinationRecord(), ResultCode.NoContent);

            return Result<List<VaccinationCardResponse>>.Success([.. vaccinationRecords.Select(vaccinationRecord => new VaccinationCardResponse(vaccinationRecord.VaccineId, vaccinationRecord.Vaccine.Name, vaccinationRecord.VaccinationDate, vaccinationRecord.DoseNumber))]);
        }
    }
}
