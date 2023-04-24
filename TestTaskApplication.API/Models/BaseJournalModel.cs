namespace TestTaskApplication.API.Models;

public class BaseJournalModel
{
    public long Id { get; set; }
    public long EventId { get; set; }
    public DateTime CreatedAt { get; set; }
}