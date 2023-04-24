using TestTaskApplication.Application.Helpers;
using TestTaskApplication.Application.IServices;
using TestTaskApplication.Core.Entities;
using TestTaskApplication.Core.Exceptions;
using TestTaskApplication.Core.Interfaces;

namespace TestTaskApplication.Application.Services;

public class NodeService : INodeService
{
    private readonly INodeRepository _nodeRepository;

    public NodeService(INodeRepository nodeRepository)
    {
        _nodeRepository = nodeRepository;
    }

    public async Task<Node> GetTreeByName(string treeName)
    {
        var nodes = await _nodeRepository.GetTreeNodesByNameAsync(treeName);
        return nodes.BuildTree();
    }

    public async Task CreateNode(string treeName, long parentNodeId, string nodeName)
    {
        if (!await _nodeRepository.IsExistNodeByTreeNameAndParentNodeIdAsync(treeName, parentNodeId))
        {
            throw new SecureException($"Node does not exist with parent node id: {parentNodeId}");
        }

        if (await _nodeRepository.IsExistNodeByTreeNameAndNodeNameAsync(treeName, nodeName))
        {
            throw new SecureException($"Node exist with node name: {nodeName}");
        }

        await _nodeRepository.CreateAsync(new Node {Name = nodeName, TreeName = treeName, ParentId = parentNodeId});
    }

    public async Task DeleteNode(string treeName, long nodeId)
    {
        var node = await _nodeRepository.GetByIdAsync(nodeId);
        if (node == null)
        {
            throw new SecureException($"Node does not exist with node id: {nodeId}");
        }

        node.Children = await _nodeRepository.GetTreeNodesByNodeIdAsync(node.Id);

        if (node.Children?.Count > 0)
        {
            throw new SecureException($"You have to delete all children nodes first for node id {node.Id}");
        }

        await _nodeRepository.RemoveAsync(node);
    }

    public async Task RenameNode(string treeName, long nodeId, string newNodeName)
    {
        if (await _nodeRepository.IsExistNodeByTreeNameAndNodeNameAsync(treeName, newNodeName))
        {
            throw new SecureException($"Node exist with node name: {newNodeName}");
        }

        var node = await _nodeRepository.GetByIdAsync(nodeId);
        if (node == null)
        {
            throw new SecureException($"Node does not exist with node id: {nodeId}");
        }

        node.Name = newNodeName;
        await _nodeRepository.UpdateAsync(node);
    }
}