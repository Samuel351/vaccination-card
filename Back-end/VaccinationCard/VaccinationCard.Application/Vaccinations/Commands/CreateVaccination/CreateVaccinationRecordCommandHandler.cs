using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Application.Vaccinations.Commands.CreateVaccination;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.VaccinationRecords.Commands.CreateVaccination
{
    public sealed class CreateVaccinationRecordCommandHandler(IBaseRepository<Vaccination> vaccinationRecordRepository) : IRequestHandler<CreateVaccinationRecordCommand, Result>
    {

        private readonly IBaseRepository<Vaccination> _vaccinationRecordRepository = vaccinationRecordRepository;

        public async Task<Result> Handle(CreateVaccinationRecordCommand request, CancellationToken cancellationToken)
        {
            await _vaccinationRecordRepository.AddAsync(new Vaccination(request.VaccineId, request.PersonId, request.DoseNumber, request.VaccinationDate));

            return Result.Success();
        }
    }
}
