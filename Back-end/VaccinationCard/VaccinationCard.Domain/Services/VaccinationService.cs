using Domain.Abstractions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;

namespace VaccinationCard.Domain.Services
{
    /// <summary>
    /// Provides domain-level validation and business rules for managing vaccination records.
    /// </summary>
    public class VaccinationService(IVaccinationRepository vaccinationRepository)
    {
        private readonly IVaccinationRepository _vaccinationRepository = vaccinationRepository;

        /// <summary>
        /// Validates whether a new vaccine dose can be registered for a specific person.
        /// </summary>
        /// <param name="vaccination">The vaccination record to validate, containing the dose number, application date, and person information.</param>
        /// <param name="vaccine">The vaccine associated with the vaccination record, containing required dose information.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the vaccination is valid:
        /// <list type="bullet">
        /// <item>
        /// <description>Returns <c>Success</c> if the vaccination can be applied.</description>
        /// </item>
        /// <item>
        /// <description>Returns <c>Failure</c> with a specific error if validation fails (e.g., dose already applied, dose sequence incorrect).</description>
        /// </item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// This method checks:
        /// <list type="number">
        /// <item>Whether the same dose number has already been applied.</item>
        /// <item>Whether the dose number exceeds the vaccine's required doses.</item>
        /// <item>Whether all previous doses have been applied in sequence.</item>
        /// <item>Whether the application date is valid compared to previous doses.</item>
        /// </list>
        /// </remarks>
        public async Task<Result> ValidateNewDose(Vaccination vaccination, Vaccine vaccine)
        {
            var vaccinations = await _vaccinationRepository.GetVaccinations(vaccination.VaccineId, vaccination.PersonId);

            // Check if this dose has already been applied
            if (vaccinations.Any(x => x.DoseNumber == vaccination.DoseNumber))
            {
                return Result.Failure(VaccinationErrors.DoseAlreadyApplied);
            }

            // Check if the dose exceeds the required number of doses for this vaccine
            if (vaccination.DoseNumber > vaccine.RequiredDoses)
            {
                return Result.Failure(VaccinationErrors.DoseExceedsRequired);
            }

            // Ensure all previous doses have been applied
            for (var doseNumber = 1; doseNumber < vaccination.DoseNumber; doseNumber++)
            {
                if (!vaccinations.Any(x => x.DoseNumber == doseNumber))
                {
                    return Result.Failure(VaccinationErrors.DoseNotAppliedYet(doseNumber));
                }
            }

            // Ensure that the application date is not in the future
            if(vaccination.ApplicationDate > DateTime.Now)
            {
                return Result.Failure(VaccinationErrors.DoseApplicationDateInFuture);
            }

            // Ensure the application date is not before any previously applied dose
            if (vaccinations.Any(x => x.ApplicationDate > vaccination.ApplicationDate))
            {
                return Result.Failure(VaccinationErrors.DoseApplicationDate);
            }

            return Result.Success();
        }
    }
}
