using MediatR;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.VaccinationRecords.Commands
{
    public sealed class CreateVaccinationRecordCommandHandler(IBaseRepository<Vaccination> vaccinationRecordRepository) : IRequestHandler<CreateVaccinationRecordCommand, Result<bool>>
    {

        private readonly IBaseRepository<Vaccination> _vaccinationRecordRepository = vaccinationRecordRepository;

        public async Task<Result<bool>> Handle(CreateVaccinationRecordCommand request, CancellationToken cancellationToken)
        {
            await _vaccinationRecordRepository.AddAsync(new Vaccination(request.VaccineId, request.PersonId, request.DoseNumber, request.VaccinationDate));

            return Result<bool>.Success(true);
        }
    }
}
