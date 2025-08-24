using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;

namespace VaccinationCard.Application.Vaccinations.Commands.CreateVaccination
{
    public sealed class CreateVaccinationRecordCommandHandler(IVaccinationRepository vaccinationRepository, IPersonRepository personRepository, IBaseRepository<Vaccine> vaccineRepository) : IRequestHandler<CreateVaccinationRecordCommand, Result>
    {

        private readonly IVaccinationRepository _vaccinationRepository = vaccinationRepository;

        private readonly IPersonRepository _personRepository = personRepository;

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        public async Task<Result> Handle(CreateVaccinationRecordCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId);

            if (person == null) return Result.Failure(PersonErrors.NotFound, HttpStatusCode.NotFound);

            var vaccine = await _vaccineRepository.GetByIdAsync(request.VaccineId);

            if (vaccine == null) return Result.Failure(VaccineErrors.NotFound, HttpStatusCode.NotFound);

            var vaccinations = await _vaccinationRepository.GetVaccinations(request.VaccineId, request.PersonId);

            if(vaccinations.Any(x => x.DoseNumber == request.DoseNumber))
            {
                return Result.Failure(VaccinationErrors.DoseAlreadyApplied);
            }

            if(request.DoseNumber > vaccine.RequiredDoses)
            {
                return Result.Failure(VaccinationErrors.DoseAlreadySurpassRequired);
            }

            // TODO: DATA DOSE NÃO PODE SER NO FUTURO E NEM ANTERIOR A DATA DE OUTRAS DOSE JÁ EXISTENTES

            for (var doseNumber = 1; doseNumber < request.DoseNumber; doseNumber++)
            {
                if (!vaccinations.Any(x => x.DoseNumber == doseNumber))
                {
                    return Result.Failure(VaccinationErrors.DoseNotAppliedYet(doseNumber));
                }
            }

            Vaccination? vaccination;

            if (!request.ApplicationDate.HasValue) vaccination = new Vaccination(request.VaccineId, request.PersonId, request.DoseNumber, DateTime.UtcNow);
            else vaccination = new Vaccination(request.VaccineId, request.PersonId, request.DoseNumber, request.ApplicationDate.Value);

            await _vaccinationRepository.AddAsync(vaccination);

            return Result.Success();
        }
    }
}
