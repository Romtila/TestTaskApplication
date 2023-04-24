namespace TestTaskApplication.Core.Entities;

public class Journal : BaseEntity
{
    public string Text { get; set; } = string.Empty;
    public long EventId { get; set; }
    public DateTime CreatedAt { get; set; }
}
