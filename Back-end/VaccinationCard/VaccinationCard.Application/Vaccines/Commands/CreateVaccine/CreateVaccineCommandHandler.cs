using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.Vaccines.Commands.CreateVaccine
{
    internal class CreateVaccineCommandHandler(IBaseRepository<Vaccine> vaccineRepository) : IRequestHandler<CreateVaccineCommand, Result>
    {

        private readonly IBaseRepository<Vaccine> _vaccineRepository = vaccineRepository;

        public async Task<Result> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
        {
            await _vaccineRepository.AddAsync(new Vaccine(request.Name, request.RequiredDoses));

            return Result.Success();
        }
    }
}
