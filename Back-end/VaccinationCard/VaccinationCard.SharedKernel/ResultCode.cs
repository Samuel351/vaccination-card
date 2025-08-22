using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.SharedKernel
{
    public enum ResultCode
    {
        Ok = 200,
        Created = 201,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        UnprocessableEntity = 422,
        InternalServerError = 500
    }
}
