using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;

namespace VaccinationCard.Application.Vaccinations.Commands.UpdateVaccination
{
    internal class UpdateVaccinationCommandHandler(IBaseRepository<Vaccination> vaccineRepository) : IRequestHandler<UpdateVaccinationCommand, Result>
    {
        private readonly IBaseRepository<Vaccination> _vaccineRepository = vaccineRepository;

        public async Task<Result> Handle(UpdateVaccinationCommand request, CancellationToken cancellationToken)
        {
            var vaccination = await _vaccineRepository.GetByIdAsync(request.VaccinationId);

            if (vaccination == null) return Result.Failure(VaccinationErrors.NotFound, HttpStatusCode.NotFound);


            // TODO: DATA DOSE NÃO PODE SER NO FUTURO E NEM ANTERIOR A DATA DE OUTRAS DOSE JÁ EXISTENTES

            if (!request.ApplicationDate.HasValue) vaccination.Update(request.DoseNumber, DateTime.UtcNow);
            else vaccination.Update(request.DoseNumber, request.ApplicationDate.Value);

            return Result.Success();
        }
    }
}
