using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Services;

namespace VaccinationCard.Application.Vaccinations.Commands.UpdateVaccination
{
    public class UpdateVaccinationCommandHandler(IBaseRepository<Vaccination> vaccinationRepository, IBaseRepository<Vaccine> vaccineRepository, VaccinationService vaccinationService) : IRequestHandler<UpdateVaccinationCommand, Result>
    {
        private readonly IBaseRepository<Vaccination> _vaccinationRepository = vaccinationRepository;

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        private readonly VaccinationService _vaccinationService = vaccinationService;

        public async Task<Result> Handle(UpdateVaccinationCommand request, CancellationToken cancellationToken)
        {
            var vaccination = await _vaccinationRepository.GetByIdAsync(request.VaccinationId);

            if (vaccination == null) return Result.Failure(VaccinationErrors.NotFound, HttpStatusCode.NotFound);

            var vaccine = await _vaccineRepository.GetByIdAsync(vaccination.VaccineId);

            if (vaccine == null) return Result.Failure(VaccineErrors.NotFound, HttpStatusCode.NotFound);

            // TODO: DATA DOSE NÃO PODE SER NO FUTURO E NEM ANTERIOR A DATA DE OUTRAS DOSE JÁ EXISTENTES
            if (!request.ApplicationDate.HasValue) vaccination.Update(request.DoseNumber, DateTime.UtcNow);
            else vaccination.Update(request.DoseNumber, request.ApplicationDate.Value);

            var result = await _vaccinationService.ValidateNewDose(vaccination, vaccine);

            if (!result.IsSuccess)
            {
                return result;
            }

            await _vaccinationRepository.UpdateAsync(vaccination);

            return Result.Success();
        }
    }
}
