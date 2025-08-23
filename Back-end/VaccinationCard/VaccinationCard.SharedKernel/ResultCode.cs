namespace VaccinationCard.SharedKernel
{
    public enum ResultCode
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        UnprocessableEntity = 422,
        InternalServerError = 500
    }
}
