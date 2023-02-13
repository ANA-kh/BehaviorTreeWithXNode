using System.Reflection;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace BT
{
    [CustomEditor(typeof(BehaviourTreeRunner))]
    public class BehaviourTreeRunnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("ShowRuntimeTree"))
            {
                if (Application.isPlaying)
                {
                    var runner = target as BehaviourTreeRunner;
                    //var graph = serializedObject.FindProperty("_treeGraphOnRuntime"); 只能拿到序列化的属性
                    var graph = runner.GetType().GetField("_treeGraphOnRuntime",BindingFlags.NonPublic | BindingFlags.Instance).GetValue(runner);
                    NodeEditorWindow.Open(graph as  BehaviourTreeGraph);
                }
            }
        }
    }
}