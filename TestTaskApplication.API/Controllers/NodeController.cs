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
    private readonly IJournalService _journalService;

    public NodeController(INodeService nodeService, IJournalService journalService)
    {
        _nodeService = nodeService;
        _journalService = journalService;
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
        try
        {
            await _nodeService.CreateNode(treeName, parentNodeId, nodeName);
            return Ok();
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

    //[EndpointName("Delete an existing node in your tree. You must specify a node ID that belongs your tree.")]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string treeName, long nodeId)
    {
        try
        {
            await _nodeService.DeleteNode(treeName, nodeId);
            return Ok();
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

    //[EndpointName("Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.")]
    [HttpPut("rename")]
    public async Task<IActionResult> RenameNode(string treeName, long nodeId, string newNodeName)
    {
        try
        {
            await _nodeService.RenameNode(treeName, nodeId, newNodeName);
            return Ok();
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