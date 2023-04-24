namespace TestTaskApplication.API.Models;

public class OptionsJournalModel
{
    public string Text { get; set; } = string.Empty;
    public long EventId { get; set; }
    public DateTime CreatedAt { get; set; }
}