using TestTaskApplication.Core.Entities;

namespace TestTaskApplication.Core.Interfaces;

public interface IJournalRepository : IBaseRepository<Journal>
{
    /// <summary>
    /// Provides the pagination API. Skip means the number of items should be skipped by server. Take means the maximum number items should be returned by server. All fields of the filter are optional.
    /// </summary>
    /// <param name="skip">The number of items should be skipped by server</param>
    /// <param name="take">The maximum number items should be returned by server</param>
    /// <param name="from">From what date to look for magazines</param>
    /// <param name="to">Until what date to look for magazines</param>
    /// <param name="search">Words to search in text</param>
    /// <returns></returns>
    Task<List<Journal>> GetMany(int skip, int take, DateTime? from, DateTime? to, string? search);
}