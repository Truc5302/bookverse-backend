namespace BookVerse.Application.Models;

public class ApiResult<T>
{
    protected ApiResult(
        bool succeeded,
        T? data,
        string message,
        IEnumerable<string>? errors = null)
    {
        Succeeded = succeeded;

        Data = data;

        Message = message;

        Errors = errors ?? new List<string>();
    }

    public bool Succeeded { get; set; }

    public T? Data { get; set; }

    public string Message { get; set; }

    public IEnumerable<string> Errors { get; set; }

    public static ApiResult<T> Success(
        T data,
        string message = "Success")
    {
        return new ApiResult<T>(
            true,
            data,
            message
        );
    }

    public static ApiResult<T> Failure(
        IEnumerable<string> errors,
        string message = "Failed")
    {
        return new ApiResult<T>(
            false,
            default,
            message,
            errors
        );
    }
}


public class ApiResult
{
    protected ApiResult(
        bool succeeded,
        string message,
        IEnumerable<string>? errors = null)
    {
        Succeeded = succeeded;

        Message = message;

        Errors = errors ?? new List<string>();
    }

    public bool Succeeded { get; set; }

    public string Message { get; set; }

    public IEnumerable<string> Errors { get; set; }

    public static ApiResult Success(
        string message = "Success")
    {
        return new ApiResult(
            true,
            message
        );
    }

    public static ApiResult Failure(
        IEnumerable<string> errors,
        string message = "Failed")
    {
        return new ApiResult(
            false,
            message,
            errors
        );
    }
}