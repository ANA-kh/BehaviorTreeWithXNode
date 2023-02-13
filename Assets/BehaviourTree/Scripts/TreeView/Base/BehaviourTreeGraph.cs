using System;
using BT;
using UnityEngine;
using XNode;
using Node = XNode.Node;

[CreateAssetMenu]
public class BehaviourTreeGraph : NodeGraph 
{
    private BehaviourTreeGraphNode root;
    public BehaviourTree Build()
    {
        return new BehaviourTree(root.BuildTreeNode());
    }
    
    public void SetRoot(BehaviourTreeGraphNode node)
    {
        if (root) { root.IsRoot = false; }
        root = node;
        root.IsRoot = true;
    }

    public override Node AddNode(Type type)
    {
        if (typeof(BehaviourTreeGraphNode).IsAssignableFrom(type) == false)
        {
            return null;
        }
        var node = base.AddNode(type) as BehaviourTreeGraphNode;
        if (root == null)
        {
            SetRoot(node);
        }

        return node;
    }
}