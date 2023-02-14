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
        private Color runningColor = new Color(0.35f,0.58f,0.35f);
        public override string GetNodeMenuName(Type type)
        {
            return base.GetNodeMenuName(type);
        }

        public override void OnGUI()
        {
            if (Application.isPlaying)
            {
                NodeEditorWindow.current.Repaint();
            }
            else
            {
                base.OnGUI();
            }
        }

        public override void AddContextMenuItems(GenericMenu menu, Type compatibleType = null,
            NodePort.IO direction = NodePort.IO.Input)
        {
            Vector2 pos = NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition);
            //string typePath = type.ToString().Replace('.', '/');

            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                menu.AddItem(new GUIContent($"[Composite]/{type.Name}"), false, () =>
                {
                    var node = CreateNode(typeof(CompositeGraphNode), pos) as CompositeGraphNode;
                    node.TreeNode = Activator.CreateInstance(type) as CompositeNode;
                    NodeEditorWindow.current.AutoConnect(node);
                });
            }

            types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {
                menu.AddItem(new GUIContent($"[Decorator]/{type.Name}"), false, () =>
                {
                    var node = CreateNode(typeof(DecoratorGraphNode), pos) as DecoratorGraphNode;
                    node.TreeNode = Activator.CreateInstance(type) as DecoratorNode;
                    NodeEditorWindow.current.AutoConnect(node);
                });
            }

            types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in types)
            {
                menu.AddItem(new GUIContent($"[Action]/{type.Name}"), false, () =>
                {
                    var node = CreateNode(typeof(ActionGraphNode), pos) as ActionGraphNode;
                    node.TreeNode = Activator.CreateInstance(type) as ActionNode;
                    NodeEditorWindow.current.AutoConnect(node);
                });
            }
            
            types = TypeCache.GetTypesDerivedFrom<ConditionNode>();
            foreach (var type in types)
            {
                menu.AddItem(new GUIContent($"[Condition]/{type.Name}"), false, () =>
                {
                    var node = CreateNode(typeof(ConditionGraphNode), pos) as ConditionGraphNode;
                    node.TreeNode = Activator.CreateInstance(type) as ConditionNode;
                    NodeEditorWindow.current.AutoConnect(node);
                });
            }
        }

        public override NoodlePath GetNoodlePath(NodePort output, NodePort input)
        {
            return NoodlePath.Angled;
        }

        public override Gradient GetNoodleGradient(NodePort output, NodePort input)
        {
            if (input == null)return base.GetNoodleGradient(output, input);
            var node = input.node as BehaviourTreeGraphNode;
            if (node.TreeNode.state == BehaviourNode.State.Running)
            {
                var grad = new Gradient();
                grad.SetKeys(new[]
                {
                    new GradientColorKey(runningColor, 0),
                    new GradientColorKey(runningColor, 1)
                }, new[]
                {
                    new GradientAlphaKey(1, 0),
                    new GradientAlphaKey(1, 1)
                });
                return grad;
            }
            else
                return base.GetNoodleGradient(output, input);
        }
    }
}