using BT;
using UnityEngine;

[NodeTint("#6b2e53")]
public class DecoratorGraphNode : BehaviourTreeGraphNode
{
    [Output(connectionType = ConnectionType.Override)] public BehaviourTreeGraphPort child;
    
    public override string Title =>"D: " + TreeNode?.GetType().Name;
	
    public override BT.Node BuildTreeNode()
    {
        var decorator = TreeNode as DecoratorNode;
        var childPort = GetOutputPort("child").GetConnection(0);
        if (child == null)
        {
            Debug.Log("Decorator has no child");
        }
		
        decorator.child = (childPort.node as BehaviourTreeGraphNode).BuildTreeNode();
        return decorator;
    }
}