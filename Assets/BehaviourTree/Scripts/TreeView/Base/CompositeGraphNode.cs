using System.Linq;
using BT;

[NodeTint("#2e4e6b")]
public class CompositeGraphNode : BehaviourTreeGraphNode
{
    [Output] public BehaviourTreeGraphPort children;

    public override BT.Node BuildTreeNode()
    {
        var composite = TreeNode as CompositeNode;
        composite.children.Clear();
        var children = GetOutputPort("children").GetConnections().Select((port)=>port.node).ToList();
        children.Sort((b, a) => a.position.y.CompareTo(b.position.x));
        foreach (var child in children)
        {
            var childNode = child as BehaviourTreeGraphNode;
            composite.children.Add(childNode.BuildTreeNode());
        }
        return composite;
    }
}