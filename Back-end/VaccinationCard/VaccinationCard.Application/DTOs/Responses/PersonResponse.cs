using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.DTOs.Responses
{
    public sealed record PersonResponse(Guid id, string Name, string CPF, string Email, string PhoneNumber, string Gender, DateTime BirthDate);
}
