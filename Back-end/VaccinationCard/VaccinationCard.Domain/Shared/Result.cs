using System.Net;

namespace VaccinationCard.Domain.Shared;

public class Result
{
    protected Result(bool isSuccess, Error error, HttpStatusCode statusCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public HttpStatusCode StatusCode { get; }

    public static Result Success(HttpStatusCode statusCode = HttpStatusCode.OK) => new(true, Error.None, statusCode);

    public static Result Failure(Error error, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new(false, error, statusCode);
}

public class Result<T> : Result
{
    private Result(T value, HttpStatusCode statusCode) : base(true, Error.None, statusCode)
    {
        Value = value;
    }

    private Result(Error error, HttpStatusCode status) : base(false, error, status)
    {
        Value = default;
    }

    public T? Value { get; }

    public static Result<T> Success(T value, HttpStatusCode statusCode = HttpStatusCode.OK) => new(value, statusCode);

    public static new Result<T> Failure(Error error, HttpStatusCode status = HttpStatusCode.BadRequest) =>new(error, status);
}

