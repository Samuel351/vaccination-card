using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Vaccines.Commands.UpdateVaccine
{
    internal class UpdateVaccineCommandHandler(IBaseRepository<Vaccine> vaccineRepository) : IRequestHandler<UpdateVaccineCommand, Result>
    {

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        public async Task<Result> Handle(UpdateVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(request.VaccineId);

            if (vaccine == null) return Result.Failure(VaccineErrors.NotFound, HttpStatusCode.NotFound);

            vaccine.Update(request.Name, request.RequiredDoses);

            await _vaccineRepository.UpdateAsync(vaccine);

            return Result.Success();
        }
    }
}
