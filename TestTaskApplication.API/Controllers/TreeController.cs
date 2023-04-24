using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using TestTaskApplication.API.Helpers;
using TestTaskApplication.Application.IServices;
using TestTaskApplication.Core.Exceptions;

namespace TestTaskApplication.API.Controllers;

[ApiController]
[Description("Represents entire tree API")]
[Route("[controller]")]
public class TreeController : ControllerBase
{
    private readonly INodeService _nodeService;
    private readonly IJournalService _journalService;

    public TreeController(INodeService nodeService, IJournalService journalService)
    {
        _nodeService = nodeService;
        _journalService = journalService;
    }

    //[EndpointName("Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.")]
    /// <summary>
    /// aaaaa 
    /// <remarks>Awesomeness!</remarks>
    /// </summary>
    /// <param name="treeName"></param>
    /// <returns></returns>
    [HttpGet("get")]
    public async Task<IActionResult> GetTree(string treeName)
    {
        try
        {
            var node = await _nodeService.GetTreeByName(treeName);
            return Ok(node);
        }
        catch (SecureException e)
        {
            var queryString = Request.QueryString.ToString();
            var requestBody = await Request.GetRawBodyAsync();
            var headers = Request.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
            await _journalService.Save(e, queryString, requestBody, headers);

            return StatusCode(500, e.Message);
        }
        catch (Exception e)
        {
            var queryString = Request.QueryString.ToString();
            var requestBody = await Request.GetRawBodyAsync();
            var headers = Request.Headers.ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
            await _journalService.Save(e, queryString, requestBody, headers);

            return StatusCode(500, e.Message);
        }
    }
}