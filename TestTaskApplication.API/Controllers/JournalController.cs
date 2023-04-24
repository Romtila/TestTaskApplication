using Microsoft.AspNetCore.Mvc;
using TestTaskApplication.API.Helpers;
using TestTaskApplication.API.Models;
using TestTaskApplication.Application.IServices;
using TestTaskApplication.Core.Exceptions;
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
        try
        {
            var journal = await _journalService.GetSingle(id);
            return Ok(journal);
        }
        catch (SecureException e)
        {
            var queryString = Request.QueryString.ToString();
            var requestBody = await Request.GetRawBodyAsync();
            var headers = Request.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
            await _journalService.Save(e, queryString, requestBody, headers);

            return StatusCode(500,
                new StatusResponse
                {
                    Type = e.GetType().Name, Id = e.HResult,
                    Data = new Dictionary<string, string> {{"message", e.Message}}
                });
        }
        catch (Exception e)
        {
            var queryString = Request.QueryString.ToString();
            var requestBody = await Request.GetRawBodyAsync();
            var headers = Request.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
            await _journalService.Save(e, queryString, requestBody, headers);

            return StatusCode(500,
                new StatusResponse
                {
                    Type = e.GetType().Name, Id = e.HResult,
                    Data = new Dictionary<string, string> {{"message", e.Message}}
                });
        }
    }

    [HttpGet("getSingle")]
    public async Task<IActionResult> GetRange(int skip, int take, JournalFilter? journalFilter)
    {
        try
        {
            var campaigns = await _journalService.GetRange(skip, take, journalFilter);
            return Ok(campaigns);
        }
        catch (SecureException e)
        {
            var queryString = Request.QueryString.ToString();
            var requestBody = await Request.GetRawBodyAsync();
            var headers = Request.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
            await _journalService.Save(e, queryString, requestBody, headers);

            return StatusCode(500,
                new StatusResponse
                {
                    Type = e.GetType().Name, Id = e.HResult,
                    Data = new Dictionary<string, string> {{"message", e.Message}}
                });
        }

        catch (Exception e)
        {
            var queryString = Request.QueryString.ToString();
            var requestBody = await Request.GetRawBodyAsync();
            var headers = Request.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
            await _journalService.Save(e, queryString, requestBody, headers);

            return StatusCode(500,
                new StatusResponse
                {
                    Type = e.GetType().Name, Id = e.HResult,
                    Data = new Dictionary<string, string> {{"message", e.Message}}
                });
        }
    }
}