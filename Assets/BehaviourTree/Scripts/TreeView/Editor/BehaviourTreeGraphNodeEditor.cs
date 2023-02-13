using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

[CustomNodeEditor(typeof(BehaviourTreeGraphNode))]
public class BehaviourTreeGraphNodeEditor : NodeEditor
{
    public override void OnHeaderGUI()
    {
        GUILayout.Label((target as BehaviourTreeGraphNode).Title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
    }

    public override void OnBodyGUI()
    {
        // Unity specifically requires this to save/update any serial object.
        // serializedObject.Update(); must go at the start of an inspector gui, and
        // serializedObject.ApplyModifiedProperties(); goes at the end.
        serializedObject.Update();
        string[] excludes = { "m_Script", "graph", "position", "ports" ,};
	   
		  
        //Iterate through serialized properties and draw them like the Inspector (But with ports)
        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren)) {
            enterChildren = false;
            if (excludes.Contains(iterator.name)) continue;
            var attribute = iterator.GetFieldInfo().GetCustomAttribute(typeof(BehaviourTreeGraphNode.NotShowInGraphNodeAttribute),false);
            if (attribute != null)
            {
                continue;
            }
            NodeEditorGUILayout.PropertyField(iterator, true);
        }
    
    
        // Iterate through dynamic ports and draw them in the order in which they are serialized
        // Iterate through
        foreach (XNode.NodePort dynamicPort in target.DynamicPorts) {
            if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
            NodeEditorGUILayout.PortField(dynamicPort);
        }
    
        serializedObject.ApplyModifiedProperties();
    }
}