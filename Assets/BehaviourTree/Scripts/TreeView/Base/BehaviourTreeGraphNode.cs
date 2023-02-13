using System;
using Unity.VisualScripting;
using UnityEngine;
using XNode;
using Node = XNode.Node;

[NodeWidth(120)]
public abstract class BehaviourTreeGraphNode : Node 
{

	[Input] public BehaviourTreeGraphPort parent;
	[SerializeReference]
	[NotShowInGraphNode]
	protected BT.Node treeNode;
	public virtual string Title => name.Replace(" Graph","") + ":" + TreeNode?.GetType().Name;

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

	public virtual BT.Node BuildTreeNode()
	{
		return TreeNode;
	}

	// public void SetTreeNode(BT.Node node)
	// {
	// 	TreeNode = node;
	// }

	public bool IsRoot { get; set; }
	public BT.Node TreeNode
	{
		get => treeNode;
		set => treeNode = value;
	}

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

	public override Node Copy()
	{
		//TODO copy treeNode
		// var node = Instantiate(this);
		// node.treeNode = treeNode.Clone();
		return base.Copy();
	}
}