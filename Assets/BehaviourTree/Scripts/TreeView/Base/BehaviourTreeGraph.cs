using System;
using BT;
using UnityEngine;
using XNode;
using Node = XNode.Node;

[CreateAssetMenu]
public class BehaviourTreeGraph : NodeGraph
{
    public BehaviourTree BehaviourTree = new BehaviourTree();
    [SerializeReference] private BehaviourTreeGraphNode root;
    public BehaviourTree Build()
    {
        if (root == null)
        {
            Debug.Log("no Root Node");
            return null;
        }
        BehaviourTree.SetRootNode(root.BuildTreeNode());
        return BehaviourTree;
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

    public override NodeGraph Copy()
    {
        var graph = base.Copy() as BehaviourTreeGraph;
        
        //SetRoot
        foreach (var node in graph.nodes)
        {
            if (node is BehaviourTreeGraphNode behaviourTreeGraphNode && behaviourTreeGraphNode.IsRoot)
            {
                graph.root = behaviourTreeGraphNode;
                break;
            }
        }
        //graph.root = graph.nodes.Find( node  => (node as BehaviourTreeGraphNode).IsRoot) as BehaviourTreeGraphNode;
        return graph;
    }
}