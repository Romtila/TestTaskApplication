using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using TestTaskApplication.Application.IServices;

namespace TestTaskApplication.API.Controllers;

[ApiController]
[Description("Represents entire tree API")]
[Route("[controller]")]
public class TreeController : ControllerBase
{
    private readonly INodeService _nodeService;

    public TreeController(INodeService nodeService)
    {
        _nodeService = nodeService;
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
        var node = await _nodeService.GetTreeByName(treeName);
        return Ok(node);
    }
}