namespace App.Base.Result;

public class Result<T>
{
    private Result(T value, bool isSuccess, string error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<T?> Success(T? value = default)
    {
        return new Result<T?>(value, true, string.Empty);
    }

    public static Result<T?> Failure(string error, T? value = default)
    {
        return new Result<T?>(value, false, error);
    }

    public bool IsSuccess { get; }

    public T Value { get; }

    public string Error { get; }
}