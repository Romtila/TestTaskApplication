using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TestTaskApplication.Application.IServices;
using TestTaskApplication.Core.Entities;

namespace TestTaskApplication.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TreeController : ControllerBase
{
    private readonly INodeService _nodeService;

    public TreeController(INodeService nodeService)
    {
        _nodeService = nodeService;
    }

    /// <summary>
    /// </summary>
    /// <remarks>Returns your entire tree. If your tree doesn't exist it will be created automatically.</remarks>
    /// <param name="treeName"></param>
    /// <returns></returns>
    [HttpGet("get")]
    [ProducesResponseType(typeof(Node), 200)]
    public async Task<IActionResult> GetTree([Required]string treeName)
    {
        var node = await _nodeService.GetTreeByName(treeName);
        return Ok(node);
    }
}