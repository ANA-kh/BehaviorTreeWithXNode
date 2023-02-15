using System;
using System.Linq;
using System.Reflection;
using BT;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

[CustomNodeEditor(typeof(BehaviourTreeGraphNode))]
public class BehaviourTreeGraphNodeEditor : NodeEditor
{
    private Color rootColor = new Color(0.42f, 0.18f, 0.18f);
    private Color unUseColor = new Color(0.5f, 0.5f, 0.5f);
    private Color runningColor = new Color(0.5f, 0.5f, 0.18f);
    private Color _color;

    public override void OnHeaderGUI()
    {
        var style = NodeEditorResources.styles.nodeHeader;
        var title = (target as BehaviourTreeGraphNode).Title;
        var size = GetTitleFontSize(title);
        style.fontSize = size;

        GUILayout.Label(title, style, GUILayout.Height(30));
        NodeEditorGUILayout.AddPortFieldCenter(target.GetInputPort("parent"));
    }

    private int GetTitleFontSize(string title)
    {
        var size = 12;
        if (title.Length > 10)
        {
            size -= (title.Length - 10) / 4;
        }

        return size;
    }

    public override void OnBodyGUI()
    {
        // Unity specifically requires this to save/update any serial object.
        // serializedObject.Update(); must go at the start of an inspector gui, and
        // serializedObject.ApplyModifiedProperties(); goes at the end.
        serializedObject.Update();
        //string[] excludes = { "m_Script", "graph", "position", "ports" ,};
	   
		  
        //Iterate through serialized properties and draw them like the Inspector (But with ports)
        // SerializedProperty iterator = serializedObject.GetIterator();
        // bool enterChildren = true;
        // while (iterator.NextVisible(enterChildren)) {
        //     enterChildren = false;
        //     if (excludes.Contains(iterator.name)) continue;
        //     var attribute = iterator.GetFieldInfo().GetCustomAttribute(typeof(BehaviourTreeGraphNode.NotShowInGraphNodeAttribute),false);
        //     if (attribute != null)
        //     {
        //         continue;
        //     }
        //     NodeEditorGUILayout.PropertyField(iterator, true);
        // }
    
    
        // Iterate through dynamic ports and draw them in the order in which they are serialized
        // Iterate through
        // foreach (XNode.NodePort dynamicPort in target.DynamicPorts) {
        //     if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
        //     NodeEditorGUILayout.PortField(dynamicPort);
        // }
        
        GUILayout.Label("",GUILayout.Height(20));
        NodeEditorGUILayout.AddPortFieldCenter(target.GetOutputPort());
    
        serializedObject.ApplyModifiedProperties();
    }

    public override Color GetTint()
    {
        if (Application.isPlaying == false) return GetCostumeTint();

        
        var node = target as BehaviourTreeGraphNode;
        if (node && node.TreeNode.state == BehaviourNode.State.Running)
        {
            return _color = runningColor;
        }
        
        var toColor = GetCostumeTint();
        
        _color = LerpColor(_color, toColor, Time.deltaTime);
        return _color;
    }

    private Color LerpColor(Color color1, Color color2, float f)
    {
        return new Color(
            Mathf.Lerp(color1.r, color2.r, f),
            Mathf.Lerp(color1.g, color2.g, f),
            Mathf.Lerp(color1.b, color2.b, f),
            Mathf.Lerp(color1.a, color2.a, f));
    }

    private Color GetCostumeTint()
    {
        var node = target as BehaviourTreeGraphNode;
        var hasParent = node.GetInputPort("parent").Connection != null;
        return node.IsRoot ? rootColor : hasParent ? base.GetTint() : base.GetTint() * unUseColor;
    }
}