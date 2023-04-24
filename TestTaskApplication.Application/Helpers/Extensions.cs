using TestTaskApplication.Core.Entities;

namespace TestTaskApplication.Application.Helpers;

public static class Extensions
{
    public static Node BuildTree(this List<Node> nodes)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        return new Node().BuildTree(nodes);
    }

    private static Node BuildTree(this Node root, List<Node> nodes)
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

    public static IEnumerable<Node> FetchChildren(this Node root, IEnumerable<Node> nodes)
    {
        return nodes.Where(n => n.ParentId == root.Id);
    }

    public static void RemoveChildren(this Node root, List<Node> nodes)
    {
        if (root.Children == null) return;
        
        foreach (var node in root.Children)
        {
            nodes.Remove(node);
        }
    }
}