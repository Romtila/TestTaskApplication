using System.Text.Json;
using TestTaskApplication.Application.IServices;
using TestTaskApplication.Application.Models;
using TestTaskApplication.Core.Entities;
using TestTaskApplication.Core.Exceptions;
using TestTaskApplication.Core.Filters;
using TestTaskApplication.Core.Interfaces;

namespace TestTaskApplication.Application.Services;

public class JournalService : IJournalService
{
    private readonly IJournalRepository _journalRepository;

    public JournalService(IJournalRepository journalRepository)
    {
        _journalRepository = journalRepository;
    }

    public async Task Save(Exception e, string query, string body, Dictionary<string, string> headers)
    {
        var statusResponseText = new StatusResponseText
        {
            StackTrace = e.StackTrace,
            Message = e.Message,
            Body = body,
            Query = query,
            Headers = headers
        };
        var text = JsonSerializer.Serialize(statusResponseText);

        await _journalRepository.CreateAsync(new Journal
        {
            CreatedAt = DateTime.UtcNow,
            EventId = e.HResult,
            Text = text
        });
    }

    public async Task<Journal?> GetSingle(long id)
    {
        var journal = await _journalRepository.GetByIdAsync(id);
        if (journal == null)
        {
            throw new SecureException($"journal does not exist with id: {id}");
        }

        return journal;
    }

    public async Task<List<Journal>?> GetRange(int skip, int take, JournalFilter? journalFilter)
    {
        var journal =
            await _journalRepository.GetMany(skip, take, journalFilter?.From, journalFilter?.To, journalFilter?.Search);
        if (journal == null)
        {
            throw new SecureException("journals does not exist with your parameters");
        }

        return journal;
    }
}