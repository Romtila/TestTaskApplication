using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TestTaskApplication.Core.Entities;
using TestTaskApplication.Core.Exceptions;
using TestTaskApplication.Core.Interfaces;
using TestTaskApplication.Infrastructure.Data;

namespace TestTaskApplication.Infrastructure.Repositories;

public class NodeRepository : BaseRepository<Node>, INodeRepository
{
    private string? _connectionString;

    public NodeRepository(ApplicationContext dbContext, IConfiguration configuration) : base(dbContext)
    {
        _connectionString = configuration.GetConnectionString("ConnectionString");
    }

    public async Task<List<Node>> GetTreeNodesByNameAsync(string treeName)
    {
        var query = @$"WITH RECURSIVE NodeSql (Id, ParentId, Name, TreeName) AS
                        (SELECT ""Id"", ""ParentId"", ""Name"", ""TreeName""
                        FROM ""Nodes"" 
                        WHERE ""Id"" = (SELECT ""Id"" FROM ""Nodes"" tn WHERE tn.""TreeName"" = '{treeName}' Order By tn.""Id"" LIMIT 1) 
                        UNION ALL
                        SELECT t.""Id"", t.""ParentId"", t.""Name"", t.""TreeName""
                        FROM NodeSql INNER JOIN ""Nodes"" t
                        ON NodeSql.Id = t.""ParentId"")
                        SELECT * FROM NodeSql";

        await using var db = new NpgsqlConnection(_connectionString);
        var nodes = db.Query<Node>(query).AsList();

        if (nodes.Count == 0)
            throw new SecureException($"Node does not exist with trees name: {treeName}");

        return nodes;
    }

    public async Task<List<Node>> GetTreeNodesByNodeIdAsync(long nodeId)
    {
        var query = @$"WITH RECURSIVE NodeSql (Id, ParentId, Name, TreeName) AS
                        (SELECT ""Id"", ""ParentId"", ""Name"", ""TreeName""
                        FROM ""Nodes"" 
                        WHERE ""Id"" = {nodeId}
                        UNION ALL
                        SELECT t.""Id"", t.""ParentId"", t.""Name"", t.""TreeName""
                        FROM NodeSql INNER JOIN ""Nodes"" t
                        ON NodeSql.Id = t.""ParentId"")
                        SELECT * FROM NodeSql";

        await using var db = new NpgsqlConnection(_connectionString);
        var nodes = db.Query<Node>(query).AsList();

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