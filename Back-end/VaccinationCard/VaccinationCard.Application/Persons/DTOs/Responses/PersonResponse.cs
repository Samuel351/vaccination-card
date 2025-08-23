using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.Persons.DTOs.Response
{
    public sealed record PersonResponse(Guid id, string Name);
}
