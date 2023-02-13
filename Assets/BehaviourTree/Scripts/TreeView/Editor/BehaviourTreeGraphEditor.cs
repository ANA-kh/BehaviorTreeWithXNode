using System;
using BT;
using BT.Actions;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace SimpleTest
{
    [CustomNodeGraphEditor(typeof(BehaviourTreeGraph))]
    public class BehaviourTreeGraphEditor : NodeGraphEditor
    {
        public override string GetNodeMenuName(Type type)
        {
            return base.GetNodeMenuName(type);
        }

        public override void AddContextMenuItems(GenericMenu menu, Type compatibleType = null, NodePort.IO direction = NodePort.IO.Input)
        {
            Vector2 pos = NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition);
            //string typePath = type.ToString().Replace('.', '/');
            
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                menu.AddItem(new GUIContent($"[Composite]/{type.Name}"), false, () =>
                {
                    var node = CreateNode(typeof(CompositeGraphNode), pos) as CompositeGraphNode;
                    node.SetTreeNode(Activator.CreateInstance(type) as CompositeNode);
                    NodeEditorWindow.current.AutoConnect(node);
                });
            }
            
            types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {
                menu.AddItem(new GUIContent($"[Decorator]/{type.Name}"), false, () =>
                {
                    var node = CreateNode(typeof(DecoratorGraphNode), pos) as DecoratorGraphNode;
                    node.SetTreeNode(Activator.CreateInstance(type) as DecoratorNode);
                    NodeEditorWindow.current.AutoConnect(node);
                });
            }
            
            types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in types)
            {
                menu.AddItem(new GUIContent($"[Action]/{type.Name}"), false, () =>
                {
                    var node = CreateNode(typeof(ActionGraphNode), pos) as ActionGraphNode;
                    node.SetTreeNode(Activator.CreateInstance(type) as ActionNode);
                    NodeEditorWindow.current.AutoConnect(node);
                });
            }

        }

        public override NoodlePath GetNoodlePath(NodePort output, NodePort input)
        {
            return NoodlePath.Angled;
        }
    }
}