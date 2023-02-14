using System.Linq;
using BT;

[NodeTint("#2e4e6b")]
public class CompositeGraphNode : BehaviourTreeGraphNode
{
    [Output] public BehaviourTreeGraphPort children;
    
    public override string Title =>"CP: " + TreeNode?.GetType().Name;

    public override BT.BehaviourNode BuildTreeNode()
    {
        var composite = TreeNode as CompositeNode;
        composite.ClearChildren();
        var children = GetOutputPort("children").GetConnections().Select((port)=>port.node).ToList();
        children.Sort((a, b) => a.position.y.CompareTo(b.position.x));
        foreach (var child in children)
        {
            var childNode = child as BehaviourTreeGraphNode;
            //composite.children.Add(childNode.BuildTreeNode());
            composite.AddChild(childNode.BuildTreeNode());
        }
        return composite;
    }
}