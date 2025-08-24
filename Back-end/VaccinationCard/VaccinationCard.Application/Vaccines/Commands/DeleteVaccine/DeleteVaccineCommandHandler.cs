using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Vaccines.Commands.DeleteVaccine
{
    internal class DeleteVaccineCommandHandler(IBaseRepository<Vaccine> vaccineRepository) : IRequestHandler<DeleteVaccineCommand, Result>
    {

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        public async Task<Result> Handle(DeleteVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(request.VaccineId);

            if (vaccine == null) return Result.Failure(VaccineErrors.NotFound, HttpStatusCode.NotFound);

            await _vaccineRepository.DeleteAsync(request.VaccineId);

            return Result.Success();
        }
    }
}
