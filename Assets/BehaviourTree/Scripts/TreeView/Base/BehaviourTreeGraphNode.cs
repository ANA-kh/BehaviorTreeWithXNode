using System;
using BT;
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
	protected BT.BehaviourNode treeNode;
	public virtual string Title => name.Replace(" Graph","") + ":" + TreeNode?.GetType().Name;

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

	public virtual BT.BehaviourNode BuildTreeNode()
	{
		return TreeNode;
	}

	// public void SetTreeNode(BT.Node node)
	// {
	// 	TreeNode = node;
	// }

	[HideInInspector]
	public bool IsRoot;
	public BT.BehaviourNode TreeNode
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

	private void OnDestroy()
	{
		treeNode.state = BehaviourNode.State.Inactive;
	}
	

	public override Node Copy()
	{
		//TODO copy treeNode
		// var node = Instantiate(this);
		// node.treeNode = treeNode.Clone();
		return base.Copy();
	}
}