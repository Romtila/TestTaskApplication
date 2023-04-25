using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TestTaskApplication.Application.IServices;

namespace TestTaskApplication.API.Controllers;

[ApiController]
[Route("tree/[controller]")]
public class NodeController : ControllerBase
{
    private readonly INodeService _nodeService;

    public NodeController(INodeService nodeService)
    {
        _nodeService = nodeService;
    }

    /// <summary>
    /// </summary>
    /// <remarks>Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.</remarks>
    /// <param name="treeName"></param>
    /// <param name="parentNodeId"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create([Required]string treeName, [Required]long parentNodeId, [Required]string nodeName)
    {
        await _nodeService.CreateNode(treeName, parentNodeId, nodeName);
        return Ok();
    }

    /// <summary>
    /// </summary>
    /// <remarks>Delete an existing node in your tree. You must specify a node ID that belongs your tree. </remarks>
    /// <param name="treeName"></param>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([Required]string treeName, [Required]long nodeId)
    {
        await _nodeService.DeleteNode(treeName, nodeId);
        return Ok();
    }

    /// <summary>
    /// </summary>
    /// <remarks>Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.</remarks>
    /// <param name="treeName"></param>
    /// <param name="nodeId"></param>
    /// <param name="newNodeName"></param>
    /// <returns></returns>
    [HttpPut("rename")]
    public async Task<IActionResult> RenameNode([Required]string treeName, [Required]long nodeId, [Required]string newNodeName)
    {
        await _nodeService.RenameNode(treeName, nodeId, newNodeName);
        return Ok();
    }
}