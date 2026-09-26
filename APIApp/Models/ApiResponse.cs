namespace APIApp.Models;

public class ApiResponse<T>
{
    public int StatusCode { get; }
    public T Data { get; }
    public string? ErrorMessage { get; }
    public bool IsSuccess => StatusCode >= 200 && StatusCode <= 299;

    public ApiResponse(int statusCode, T data = default!, string? errorMessage = null)
    {
        StatusCode = statusCode;
        Data = data;
        ErrorMessage = errorMessage;
    }
}