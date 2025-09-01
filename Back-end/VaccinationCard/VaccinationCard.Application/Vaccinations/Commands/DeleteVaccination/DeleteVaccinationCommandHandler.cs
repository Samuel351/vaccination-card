using MediatR;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Vaccinations.Commands.DeleteVaccination
{
    internal class DeleteVaccinationCommandHandler(IBaseRepository<Vaccination> vaccinationRepository) : IRequestHandler<DeleteVaccinationCommand, Result>
    {

        private readonly IBaseRepository<Vaccination> _vaccinationRepository = vaccinationRepository;

        public async Task<Result> Handle(DeleteVaccinationCommand request, CancellationToken cancellationToken)
        {
            var vaccination = await _vaccinationRepository.GetByIdAsync(request.VaccinationId, cancellationToken);

            if (vaccination == null) return Result.Failure(VaccinationErrors.NotFound);

            await _vaccinationRepository.DeleteAsync(request.VaccinationId, cancellationToken);

            return Result.Success();
        }
    }
}
