using MediatR;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.VaccinationRecords.Commands
{
    public sealed class CreateVaccinationRecordCommandHandler(IBaseRepository<VaccinationRecord> vaccinationRecordRepository) : IRequestHandler<CreateVaccinationRecordCommand, Result<bool>>
    {

        private readonly IBaseRepository<VaccinationRecord> _vaccinationRecordRepository = vaccinationRecordRepository;

        public async Task<Result<bool>> Handle(CreateVaccinationRecordCommand request, CancellationToken cancellationToken)
        {
            await _vaccinationRecordRepository.AddAsync(new VaccinationRecord(request.VaccineId, request.PersonId, request.DoseNumber, request.VaccinationDate));

            return Result<bool>.Success(true);
        }
    }
}
