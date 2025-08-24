using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Vaccines.Queries.GetAllVaccines
{
    public sealed class GetAllVaccineQueryHandler(IBaseRepository<Vaccine> vaccineRepository) : IRequestHandler<GetAllVaccineQuery, Result<List<VaccineResponse>>>
    {

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        public async Task<Result<List<VaccineResponse>>> Handle(GetAllVaccineQuery request, CancellationToken cancellationToken)
        {
            var vaccines = await _vaccineRepository.GetAllAsync();

            if (vaccines.Count == 0) return Result<List<VaccineResponse>>.Failure(VaccinesErrors.NotFound, HttpStatusCode.NoContent);

            var vaccinesResponse = vaccines.Select(vaccine => new VaccineResponse(vaccine.EntityId, vaccine.Name, vaccine.RequiredDoses)).ToList();

            return Result<List<VaccineResponse>>.Success(vaccinesResponse);
        }
    }
}
