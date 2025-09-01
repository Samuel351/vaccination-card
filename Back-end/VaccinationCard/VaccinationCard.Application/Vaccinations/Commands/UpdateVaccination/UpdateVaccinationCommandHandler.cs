using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Services;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Vaccinations.Commands.UpdateVaccination
{
    public class UpdateVaccinationCommandHandler(IBaseRepository<Vaccination> vaccinationRepository, IBaseRepository<Vaccine> vaccineRepository, VaccinationService vaccinationService) : IRequestHandler<UpdateVaccinationCommand, Result>
    {
        private readonly IBaseRepository<Vaccination> _vaccinationRepository = vaccinationRepository;

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        private readonly VaccinationService _vaccinationService = vaccinationService;

        public async Task<Result> Handle(UpdateVaccinationCommand request, CancellationToken cancellationToken)
        {
            var vaccination = await _vaccinationRepository.GetByIdAsync(request.VaccinationId, cancellationToken);

            if (vaccination == null) return Result.Failure(VaccinationErrors.NotFound, HttpStatusCode.NotFound);

            var vaccine = await _vaccineRepository.GetByIdAsync(vaccination.VaccineId, cancellationToken);

            if (vaccine == null) return Result.Failure(VaccineErrors.NotFound, HttpStatusCode.NotFound);

            if (!request.ApplicationDate.HasValue) vaccination.Update(DateTime.UtcNow);
            else vaccination.Update(request.ApplicationDate.Value);

            var result = await _vaccinationService.ValidateNewDose(vaccination, vaccine, cancellationToken);

            if (!result.IsSuccess)
            {
                return result;
            }

            await _vaccinationRepository.UpdateAsync(vaccination, cancellationToken);

            return Result.Success();
        }
    }
}
