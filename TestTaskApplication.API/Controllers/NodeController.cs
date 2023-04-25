using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using TestTaskApplication.API.Helpers;
using TestTaskApplication.API.Models;
using TestTaskApplication.Application.IServices;
using TestTaskApplication.Core.Exceptions;

namespace TestTaskApplication.API.Controllers;

/// <summary>
/// Represents tree node API
/// </summary>
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
    /// Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.
    /// </summary>
    /// <param name="treeName"></param>
    /// <param name="parentNodeId"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create(string treeName, long parentNodeId, string nodeName)
    {
        await _nodeService.CreateNode(treeName, parentNodeId, nodeName);
        return Ok();
    }

    //[EndpointName("Delete an existing node in your tree. You must specify a node ID that belongs your tree.")]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string treeName, long nodeId)
    {
        await _nodeService.DeleteNode(treeName, nodeId);
        return Ok();
    }

    //[EndpointName("Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.")]
    [HttpPut("rename")]
    public async Task<IActionResult> RenameNode(string treeName, long nodeId, string newNodeName)
    {
        await _nodeService.RenameNode(treeName, nodeId, newNodeName);
        return Ok();
    }
}