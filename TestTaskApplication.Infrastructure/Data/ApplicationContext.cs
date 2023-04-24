using Microsoft.EntityFrameworkCore;
using TestTaskApplication.Core.Entities;

namespace TestTaskApplication.Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.DefaultTypeMapping<Node>();
    }

    public DbSet<Journal> Journals { get; set; }
    public DbSet<Node> Nodes { get; set; }
}