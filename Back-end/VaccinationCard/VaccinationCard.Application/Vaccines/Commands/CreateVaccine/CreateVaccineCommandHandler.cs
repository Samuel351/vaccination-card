using MediatR;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Vaccines.Commands.CreateVaccine
{
    internal class CreateVaccineCommandHandler(IBaseRepository<Vaccine> vaccineRepository) : IRequestHandler<CreateVaccineCommand, Result<bool>>
    {

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        public async Task<Result<bool>> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
        {
            await _vaccineRepository.AddAsync(new Vaccine(request.Name, request.RequiredDoses));

            return Result<bool>.Success(true, ResultCode.Created);
        }
    }
}
