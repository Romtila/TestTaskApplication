namespace TestTaskApplication.Core.Filters;

public class JournalFilter
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Search { get; set; }
}