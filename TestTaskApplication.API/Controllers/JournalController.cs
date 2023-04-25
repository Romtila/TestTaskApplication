using Microsoft.AspNetCore.Mvc;
using TestTaskApplication.Application.IServices;
using TestTaskApplication.Core.Filters;

namespace TestTaskApplication.API.Controllers;

[ApiController]
[Route("[controller]")]
public class JournalController : ControllerBase
{
    private readonly IJournalService _journalService;

    public JournalController(IJournalService journalService)
    {
        _journalService = journalService;
    }

    [HttpGet("getRange")]
    public async Task<IActionResult> GetSingle(long id)
    {
        var journal = await _journalService.GetSingle(id);
        return Ok(journal);
    }

    [HttpGet("getSingle")]
    public async Task<IActionResult> GetRange(int skip, int take, JournalFilter? journalFilter)
    {
        var campaigns = await _journalService.GetRange(skip, take, journalFilter);
        return Ok(campaigns);
    }
}