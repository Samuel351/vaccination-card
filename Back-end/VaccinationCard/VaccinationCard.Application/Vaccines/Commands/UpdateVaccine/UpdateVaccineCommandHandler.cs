using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;

namespace VaccinationCard.Application.Vaccines.Commands.UpdateVaccine
{
    public class UpdateVaccineCommandHandler(IVaccineRepository vaccineRepository, IVaccinationRepository vaccinationRepository) : IRequestHandler<UpdateVaccineCommand, Result>
    {

        private readonly IVaccineRepository _vaccineRepository = vaccineRepository;

        private readonly IVaccinationRepository _vaccinationRepository = vaccinationRepository;

        public async Task<Result> Handle(UpdateVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(request.VaccineId);

            if (vaccine == null) return Result.Failure(VaccineErrors.NotFound, HttpStatusCode.NotFound);

            // Se o nome da vacina mudar, ele verifica já tem um nome desse.
            if(request.Name != vaccine.Name)
            {
                if (await _vaccineRepository.NameExists(request.Name))
                {
                    return Result.Failure(VaccineErrors.VaccineAlreadyRegistered);
                }
            }

            if (await _vaccinationRepository.IsVaccineBeingUsed(request.VaccineId))
            {
                return Result.Failure(VaccineErrors.VaccineIsBeingUsed, HttpStatusCode.Conflict);
            }

            vaccine.Update(request.Name, request.RequiredDoses);

            await _vaccineRepository.UpdateAsync(vaccine);

            return Result.Success();
        }
    }
}
