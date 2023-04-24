namespace TestTaskApplication.Application.Models;

public class StatusResponseText
{
    public string? StackTrace { get; set; } = string.Empty;
    public string? Message { get; set; } = string.Empty;
    public string Query { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public Dictionary<string, string> Headers = new();
}