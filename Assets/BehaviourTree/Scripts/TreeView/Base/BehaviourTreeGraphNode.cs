using System;
using UnityEngine;
using XNode;
using Node = XNode.Node;

public abstract class BehaviourTreeGraphNode : Node 
{

	[Input] public BehaviourTreeGraphPort parent;
	[SerializeReference]
	[NotShowInGraphNode]
	protected BT.Node treeNode;
	public string Title => name + ":" + treeNode?.GetType().Name;

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

	public virtual BT.Node BuildTreeNode()
	{
		return treeNode;
	}

	public virtual void SetTreeNode(BT.Node node)
	{
		treeNode = node;
	}

	public bool IsRoot { get; set; }
	
	[ContextMenu("Set as root")]
	public void SetAsRoot()
	{
		BehaviourTreeGraph btGraph = graph as BehaviourTreeGraph;
		btGraph.SetRoot(this);

		NodePort port = GetInputPort("parent");
		port.Disconnect(port.Connection);
	}
	
	[AttributeUsage(AttributeTargets.Field)]
	public class NotShowInGraphNodeAttribute : Attribute
	{
		
	}
}