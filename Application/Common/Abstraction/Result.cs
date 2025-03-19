namespace Application.Common.Abstraction;

public class Result
{
    public bool IsSuccess { get; }
    
    public bool IsFailure => !IsSuccess;
    
    public string Error { get; }

    protected Result(bool isSuccess, string error)
    {
        switch (isSuccess)
        {
            case true when !string.IsNullOrEmpty(error):
                throw new InvalidOperationException("Successful result cannot have an error message.");
            case false when string.IsNullOrEmpty(error):
                throw new InvalidOperationException("Failed result must have an error message.");
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}

public class Result<TValue> : Result
{
    public TValue Value { get; }

    protected Result(bool isSuccess, TValue value, string error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<TValue> Success(TValue value) => new(true, value, null);
    public new static Result<TValue> Failure(string error) => new(false, default, error);
}