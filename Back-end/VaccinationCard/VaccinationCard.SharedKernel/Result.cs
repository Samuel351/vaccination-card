namespace VaccinationCard.SharedKernel
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public T? Value { get; }
        public ResultCode StatusCode { get; }

        // Construtor para caso de sucessos
        private Result(bool isSuccess, T value, ResultCode statusCode)
        {
            IsSuccess = isSuccess;
            Value = value;
            StatusCode = statusCode;
        }

        // Construtor para caso de erros
        public Result(bool isSuccess, string error, ResultCode statusCode)
        {
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T value, ResultCode statusCode = ResultCode.Ok) =>
            new(true, value, statusCode);

        public static Result<T> Failure(string error, ResultCode statusCode = ResultCode.BadRequest) =>
            new(false, error, statusCode);
    }
}
