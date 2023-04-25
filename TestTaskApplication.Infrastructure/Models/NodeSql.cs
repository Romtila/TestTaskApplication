using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskApplication.Infrastructure.Models;

public class NodeSql
{
    public long Id { get; set; }
    public string TreeName { get; set; } = string.Empty;
    public string  Name { get; set; } = string.Empty;
    public long? ParentId { get; set; }
}