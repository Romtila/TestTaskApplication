using Microsoft.EntityFrameworkCore;
using TestTaskApplication.Core.Entities;
using TestTaskApplication.Infrastructure.Configurations;

namespace TestTaskApplication.Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NodeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Journal> Journals { get; set; }
    public DbSet<Node> Nodes { get; set; }
}