using Microsoft.EntityFrameworkCore;
using TestTaskApplication.Core.Entities;
using TestTaskApplication.Core.Interfaces;
using TestTaskApplication.Infrastructure.Data;

namespace TestTaskApplication.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly ApplicationContext DbContext;

    protected BaseRepository(ApplicationContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        DbContext.Set<T>().Update(entity);
        await DbContext.SaveChangesAsync();
    }
}