using TestTaskApplication.Core.Entities;

namespace TestTaskApplication.Application.IServices;

public interface INodeService
{
    /// <summary>
    /// Returns your entire tree. If your tree doesn't exist it will be created automatically.
    /// </summary>
    /// <param name="treeName">Tree's name</param>
    /// <returns></returns>
    Task<Node> GetTreeByName(string treeName);

    /// <summary>
    /// Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.
    /// </summary>
    /// <param name="treeName">Tree's name</param>
    /// <param name="parentNodeId">Parent node's identifier</param>
    /// <param name="nodeName">A new node name must be unique across all siblings</param>
    /// <returns></returns>
    Task CreateNode(string treeName, long parentNodeId, string nodeName);

    /// <summary>
    /// Delete an existing node in your tree. You must specify a node ID that belongs your tree.
    /// </summary>
    /// <param name="treeName">Tree's name</param>
    /// <param name="nodeId">Node's identifier</param>
    /// <returns></returns>
    Task DeleteNode(string treeName, long nodeId);

    /// <summary>
    /// Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.
    /// </summary>
    /// <param name="treeName">Tree's name</param>
    /// <param name="nodeId">Node's identifier</param>
    /// <param name="newNodeName">A new name of the node must be unique across all siblings</param>
    /// <returns></returns>
    Task RenameNode(string treeName, long nodeId, string newNodeName);
}