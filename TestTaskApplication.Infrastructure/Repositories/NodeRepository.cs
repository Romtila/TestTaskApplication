using Microsoft.EntityFrameworkCore;
using TestTaskApplication.Core.Entities;
using TestTaskApplication.Core.Exceptions;
using TestTaskApplication.Core.Interfaces;
using TestTaskApplication.Infrastructure.Data;

namespace TestTaskApplication.Infrastructure.Repositories;

public class NodeRepository : BaseRepository<Node>, INodeRepository
{
    public NodeRepository(ApplicationContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Node>> GetTreeNodesByNameAsync(string treeName)
    {
        var nodes = await DbContext.Database
            .SqlQuery<Node>(@$"WITH RECURSIVE r (Id, ParentId, Name, TreeName, level) AS
                        (SELECT ""Id"", ""ParentId"", ""Name"", ""TreeName"", 1
                        FROM ""Nodes"" 
                        WHERE ""Id"" = (SELECT ""Id"" FROM ""Nodes"" tn WHERE tn.""TreeName"" = '{treeName}' Order By tn.""Id"" LIMIT 1) 
                        UNION ALL
                        SELECT t.""Id"", t.""ParentId"", t.""Name"", t.""TreeName"", r.level + 1
                        FROM r INNER JOIN ""Nodes"" t
                        ON r.Id = t.""ParentId"")
                        SELECT r.Id, r.TreeName, r.Name, r.ParentId FROM r")
            .ToListAsync();

        if (nodes.Count == 0)
            throw new SecureException($"Node does not exist with trees name: {treeName}");

        return nodes;
    }

    public async Task<List<Node>> GetTreeNodesByNodeIdAsync(long nodeId)
    {
        var nodes = await DbContext.Database
            .SqlQuery<Node>(@$"WITH RECURSIVE r (Id, ParentId, Name, TreeName, level) AS
                        (SELECT ""Id"", ""ParentId"", ""Name"", ""TreeName"", 1
                        FROM ""Nodes"" 
                        WHERE ""Id"" = {nodeId}
                        UNION ALL
                        SELECT t.""Id"", t.""ParentId"", t.""Name"", t.""TreeName"", r.level + 1
                        FROM r INNER JOIN ""Nodes"" t
                        ON r.Id = t.""ParentId"")
                        SELECT r.Id, r.TreeName, r.Name, r.ParentId FROM r")
            .ToListAsync();

        if (nodes.Count == 0)
            throw new SecureException($"Node does not exist with id: {nodeId}");

        return nodes;
    }

    public async Task<Node?> GetNodeByTreeNameAndParentNodeIdAsync(string treeName, long parentNodeId)
    {
        var parentNode = await DbContext.Nodes
            .FirstOrDefaultAsync(x => x.TreeName == treeName && x.ParentId == parentNodeId);

        if (parentNode == null)
            throw new SecureException($"Node does not exist with parent id: {parentNodeId} in tree: {treeName}");

        return parentNode;
    }

    public async Task<Node?> GetNodeByTreeNameAndNodeNameAsync(string treeName, string nodeName)
    {
        var parentNode = await DbContext.Nodes
            .FirstOrDefaultAsync(x => x.TreeName == treeName && x.Name == nodeName);

        if (parentNode == null)
            throw new SecureException($"Node does not exist with node name: {nodeName} in tree: {treeName}");

        return parentNode;
    }

    public async Task<bool> IsExistNodeByTreeNameAndNodeNameAsync(string treeName, string nodeName)
    {
        var parentNode = await DbContext.Nodes
            .FirstOrDefaultAsync(x => x.TreeName == treeName && x.Name == nodeName);

        return parentNode != null;
    }

    public async Task<bool> IsExistNodeByTreeNameAndParentNodeIdAsync(string treeName, long parentNodeId)
    {
        var parentNode = await DbContext.Nodes
            .FirstOrDefaultAsync(x => x.TreeName == treeName && x.ParentId == parentNodeId);

        return parentNode != null;
    }
}