namespace E_HostpitalServer.Core.Utilities.Results;

public sealed class Result<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessages { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public int StatusCode { get; set; } = 200;


    public Result(T data)
    {
        Data = data;
    }

    public Result(int statusCode, List<string> errorMessages)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
    }
    public Result(int statusCode, string errorMessage)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = [errorMessage];
    }

    public static implicit operator Result<T>(T data)
    {
        return new Result<T>(data);
    }

    public static implicit operator Result<T>((int statusCode, List<string> errorMessages) parameters)
    {
        return new Result<T>(parameters.statusCode, parameters.errorMessages);
    }
    
    public static implicit operator Result<T>((int statusCode, string errorMessage) parameters)
    {
        return new Result<T>(parameters.statusCode, parameters.errorMessage);
    }
}