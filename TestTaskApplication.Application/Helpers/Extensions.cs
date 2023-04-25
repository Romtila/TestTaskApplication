using TestTaskApplication.Application.Models;

namespace TestTaskApplication.Application.Helpers;

public static class Extensions
{
    public static NodeReadModel BuildTree(this List<NodeReadModel> nodes)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        return nodes[0].BuildTree(nodes);
    }

    private static NodeReadModel BuildTree(this NodeReadModel root, List<NodeReadModel> nodes)
    {
        if (nodes.Count == 0)
        {
            return root;
        }

        var children = root.FetchChildren(nodes).ToList();
        root.Children?.AddRange(children);
        root.RemoveChildren(nodes);

        for (var i = 0; i < children.Count; i++)
        {
            children[i] = children[i].BuildTree(nodes);
            if (nodes.Count == 0)
            {
                break;
            }
        }

        return root;
    }

    public static IEnumerable<NodeReadModel> FetchChildren(this NodeReadModel root, IEnumerable<NodeReadModel> nodes)
    {
        return root.Id == 0
            ? nodes.Where(n => n.ParentId == null)
            : nodes.Where(n => n.ParentId == root.Id);
    }

    public static void RemoveChildren(this NodeReadModel root, List<NodeReadModel> nodes)
    {
        if (root.Children == null) return;

        foreach (var node in root.Children)
        {
            nodes.Remove(node);
        }
    }
}