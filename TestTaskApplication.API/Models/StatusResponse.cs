namespace TestTaskApplication.API.Models;

public class StatusResponse
{
    public string Type { get; set; } = string.Empty;
    public long Id { get; set; }
    public Dictionary<string, string> Data { get; set; } = new();
}