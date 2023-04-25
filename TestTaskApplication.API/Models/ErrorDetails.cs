using System.Text.Json;

namespace TestTaskApplication.API.Models;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public long Id { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}