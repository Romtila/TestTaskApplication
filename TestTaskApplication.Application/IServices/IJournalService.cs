using TestTaskApplication.Core.Entities;
using TestTaskApplication.Core.Filters;

namespace TestTaskApplication.Application.IServices;

public interface IJournalService
{
    Task Save(Exception e, string query, string body, Dictionary<string, string> headers);
    Task<Journal?> GetSingle(long id);
    Task<List<Journal>?> GetRange(int skip, int take, JournalFilter? journalFilter);
}