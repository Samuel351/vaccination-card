using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Services;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Vaccinations.Commands.CreateVaccination
{
    public sealed class CreateVaccinationCommandHandler(VaccinationService vaccinationService, IVaccinationRepository vaccinationRepository, IPersonRepository personRepository, IBaseRepository<Vaccine> vaccineRepository) : IRequestHandler<CreateVaccinationCommand, Result>
    {
        private readonly VaccinationService _vaccinationService = vaccinationService;

        private readonly IVaccinationRepository _vaccinationRepository = vaccinationRepository;

        private readonly IPersonRepository _personRepository = personRepository;

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        public async Task<Result> Handle(CreateVaccinationCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId);

            if (person == null) return Result.Failure(PersonErrors.NotFound, HttpStatusCode.NotFound);

            var vaccine = await _vaccineRepository.GetByIdAsync(request.VaccineId);

            if (vaccine == null) return Result.Failure(VaccineErrors.NotFound, HttpStatusCode.NotFound);

            Vaccination? vaccination;

            if (!request.ApplicationDate.HasValue) vaccination = new Vaccination(request.VaccineId, request.PersonId, request.DoseNumber, DateTime.UtcNow);
            else vaccination = new Vaccination(request.VaccineId, request.PersonId, request.DoseNumber, request.ApplicationDate.Value);

            var result = await _vaccinationService.ValidateNewDose(vaccination, vaccine);

            if(!result.IsSuccess)
            {
                return result;
            }

            await _vaccinationRepository.AddAsync(vaccination);

            return Result.Success();
        }
    }
}
