using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;

namespace VaccinationCard.Application.Vaccines.Commands.DeleteVaccine
{
    public class DeleteVaccineCommandHandler(IBaseRepository<Vaccine> vaccineRepository, IVaccinationRepository vaccinationRepository) : IRequestHandler<DeleteVaccineCommand, Result>
    {

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        private readonly IVaccinationRepository _vaccinationRepository = vaccinationRepository;

        public async Task<Result> Handle(DeleteVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(request.VaccineId);

            if (vaccine == null) return Result.Failure(VaccineErrors.NotFound, HttpStatusCode.NotFound);

            if(await _vaccinationRepository.IsVaccineBeingUsed(request.VaccineId))
            {
                return Result.Failure(VaccineErrors.VaccineIsBeingUsed, HttpStatusCode.Conflict);
            }

            await _vaccineRepository.DeleteAsync(request.VaccineId);

            return Result.Success();
        }
    }
}
