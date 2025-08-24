namespace Domain.Abstractions;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    private Result(T value) : base(true, Error.None)
    {
        Value = value;
    }

    public T? Value { get; }

    public static Result<T> Success(T value) => new(value);
}

