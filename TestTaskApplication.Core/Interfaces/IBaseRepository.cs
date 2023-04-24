using TestTaskApplication.Core.Entities;

namespace TestTaskApplication.Core.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
     Task<T?> GetByIdAsync(long id);
     Task UpdateAsync(T entity);
     Task CreateAsync(T entity);
     Task RemoveAsync(T entity);
 }