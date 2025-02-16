namespace HomeService.Domain.Core.Entities;

public class Result(bool success, string? message = null)
{
    public bool Success { get; set; } = success;
    public string? Message { get; set; } = message;
    public static Result Ok(string? message = null) => new(true, message);
    public static Result Fail(string message) => new(false, message);
}