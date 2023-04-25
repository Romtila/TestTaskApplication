using TestTaskApplication.Core.Entities;

namespace TestTaskApplication.Application.Models;

public class NodeReadModel : BaseEntity
{
    public string TreeName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public long? ParentId { get; set; }
    public List<NodeReadModel>? Children { get; set; } = new();
}