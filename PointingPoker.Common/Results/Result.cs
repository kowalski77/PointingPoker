namespace PointingPoker.Common.Results;

public class Result
{
    protected Result(string error)
    {
        this.Success = false;
        this.Error = error;
    }

    protected Result()
    {
        this.Success = true;
    }

    public string? Error { get; set; }

    public bool Success { get; }

    public bool Failure => !this.Success;

    public static Result Ok() => new();

    public static Result Fail(string error)
    {
        return new(error);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new(value);
    }

    public static Result<T> Fail<T>(string error)
    {
        return new(error);
    }
}
