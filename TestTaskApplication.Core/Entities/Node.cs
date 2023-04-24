namespace TestTaskApplication.Core.Entities;

public class Node : BaseEntity
{
    public string TreeName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Node? Parent { get; set; }
    public long? ParentId { get; set; }
    public List<Node>? Children { get; set; }
}