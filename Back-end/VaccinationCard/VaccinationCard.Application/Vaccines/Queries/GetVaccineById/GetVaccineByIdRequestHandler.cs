using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Vaccines.Queries.GetVaccineById
{
    internal class GetVaccineByIdRequestHandler(IBaseRepository<Vaccine> vaccineRepository) : IRequestHandler<GetVaccineByIdQuery, Result<VaccineResponse>>
    {

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        public async Task<Result<VaccineResponse>> Handle(GetVaccineByIdQuery request, CancellationToken cancellationToken)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(request.VaccineId);

            if (vaccine == null) return Result<VaccineResponse>.Failure(VaccineErrors.NotFound, HttpStatusCode.NotFound);

            return Result<VaccineResponse>.Success(new VaccineResponse(vaccine.EntityId, vaccine.Name, vaccine.RequiredDoses));
        }
    }

}
