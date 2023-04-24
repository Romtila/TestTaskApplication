using Microsoft.EntityFrameworkCore;
using TestTaskApplication.Core.Entities;
using TestTaskApplication.Core.Filters;
using TestTaskApplication.Core.Interfaces;
using TestTaskApplication.Infrastructure.Data;

namespace TestTaskApplication.Infrastructure.Repositories;

public class JournalRepository : BaseRepository<Journal>, IJournalRepository
{
    public JournalRepository(ApplicationContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Journal>> GetMany(int skip, int take, DateTime? from, DateTime? to, string? search)
    {
        return await DbContext.Journals
            .Where(x => (from != null && x.CreatedAt >= from)
                        || (to != null && x.CreatedAt <= to)
                        || (search != null && x.Text.Contains(search)))
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}