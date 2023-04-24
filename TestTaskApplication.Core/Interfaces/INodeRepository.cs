using TestTaskApplication.Core.Entities;

namespace TestTaskApplication.Core.Interfaces;

public interface INodeRepository : IBaseRepository<Node>
{
    Task<List<Node>> GetTreeNodesByNameAsync(string treeName);
    Task<List<Node>> GetTreeNodesByNodeIdAsync(long nodeId);
    Task<Node?> GetNodeByTreeNameAndParentNodeIdAsync(string treeName, long parentNodeId);
    Task<Node?> GetNodeByTreeNameAndNodeNameAsync(string treeName, string nodeName);
    Task<bool> IsExistNodeByTreeNameAndNodeNameAsync(string treeName, string nodeName);
    Task<bool> IsExistNodeByTreeNameAndParentNodeIdAsync(string treeName, long parentNodeId);
}
