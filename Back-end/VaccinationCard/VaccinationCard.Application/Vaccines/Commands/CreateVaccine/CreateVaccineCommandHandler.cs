using MediatR;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Vaccines.Commands.CreateVaccine
{
    public class CreateVaccineCommandHandler(IVaccineRepository vaccineRepository) : IRequestHandler<CreateVaccineCommand, Result>
    {

        private readonly IVaccineRepository _vaccineRepository = vaccineRepository;

        public async Task<Result> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
        {
            if (await _vaccineRepository.NameExists(request.Name, cancellationToken))
            {
                return Result.Failure(VaccineErrors.VaccineAlreadyRegistered);
            }

            await _vaccineRepository.AddAsync(new Vaccine(request.Name, request.RequiredDoses), cancellationToken);

            return Result.Success();
        }
    }
}
