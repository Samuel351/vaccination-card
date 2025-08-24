using Domain.Abstractions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;

namespace VaccinationCard.Domain.Services
{
    public class VaccinationService(IVaccinationRepository vaccinationRepository)
    {

        private IVaccinationRepository _vaccinationRepository = vaccinationRepository;

        /// <summary>
        /// Validate if the new dose can be registered
        /// </summary>
        /// <param name="vaccination"></param>
        /// <param name="vaccine"></param>
        /// <returns></returns>
        public async Task<Result> ValidateNewDose(Vaccination vaccination, Vaccine vaccine)
        {
            var vaccinations = await _vaccinationRepository.GetVaccinations(vaccination.VaccineId, vaccination.PersonId);

            if (vaccinations.Any(x => x.DoseNumber == vaccination.DoseNumber))
            {
                return Result.Failure(VaccinationErrors.DoseAlreadyApplied);
            }

            if (vaccination.DoseNumber > vaccine.RequiredDoses)
            {
                return Result.Failure(VaccinationErrors.DoseAlreadySurpassRequired);
            }

            for (var doseNumber = 1; doseNumber < vaccination.DoseNumber; doseNumber++)
            {
                if (!vaccinations.Any(x => x.DoseNumber == doseNumber))
                {
                    return Result.Failure(VaccinationErrors.DoseNotAppliedYet(doseNumber));
                }
            }

            // TODO: DATA DOSE NÃO PODE SER NO FUTURO E NEM ANTERIOR A DATA DE OUTRAS DOSE JÁ EXISTENTES

            return Result.Success();
        }
    }
}
