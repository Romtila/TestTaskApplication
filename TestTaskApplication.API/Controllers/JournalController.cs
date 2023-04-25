using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TestTaskApplication.Application.IServices;
using TestTaskApplication.Core.Entities;
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

    /// <summary>
    /// </summary>
    /// <remarks>Returns the information about an particular event by ID.</remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("getSingle")]
    [ProducesResponseType(typeof(Journal), 200)]
    public async Task<IActionResult> GetSingle([Required]long id)
    {
        var journal = await _journalService.GetSingle(id);
        return Ok(journal);
    }

    /// <summary>
    /// </summary>
    /// <remarks>Provides the pagination API. Skip means the number of items should be skipped by server. Take means the maximum number items should be returned by server. All fields of the filter are optional.</remarks>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="journalFilter"></param>
    /// <returns></returns>
    [HttpGet("getRange")]
    [ProducesResponseType(typeof(List<Journal>), 200)]
    public async Task<IActionResult> GetRange([Required]int skip, [Required]int take, [Required][FromBody]JournalFilter journalFilter)
    {
        var campaigns = await _journalService.GetRange(skip, take, journalFilter);
        return Ok(campaigns);
    }
}