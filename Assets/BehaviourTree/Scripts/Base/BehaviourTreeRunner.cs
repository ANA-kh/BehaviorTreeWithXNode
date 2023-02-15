using System;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace BT
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        public BehaviourTreeGraph TreeGraph;
        private BehaviourTree _behaviourTree;
        private BehaviourTreeGraph _treeGraphOnRuntime;
        public float TickTime = 0.2f;
        private float _timer;
        public BehaviourTree BTree
        {
            get => _behaviourTree;
            set => _behaviourTree = value;
        }

        private void OnEnable()
        {
            BuildRunTimeTree();
        }

        private void BuildRunTimeTree()
        {
            _treeGraphOnRuntime = (BehaviourTreeGraph)TreeGraph.Copy();
            BTree = _treeGraphOnRuntime.Build();
            BTree.Init(gameObject);
        }

#if UNITY_EDITOR
        private void Start()
        {
            if ((Selection.activeObject as GameObject) == gameObject)
            {
                OpenNodeEditorWindow();
            }
        }
#endif

        private void Update()
        {
            if (BTree.TreeState == BehaviourNode.State.Running)
            {
                _timer += Time.deltaTime;
                if (_timer > TickTime)
                {
                    _timer -= TickTime;
                    BTree.Update();
                }
            }
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            Selection.selectionChanged -= OnSelectionChanged;
            Selection.selectionChanged += OnSelectionChanged;
            // EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            // EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnSelectionChanged()
        {
            var treeRunner = (Selection.activeObject as GameObject)?.GetComponent<BehaviourTreeRunner>();
            if (treeRunner)
            {
                treeRunner.OpenNodeEditorWindow();
            }
        }

        public void OpenNodeEditorWindow()
        {
            if (Application.isPlaying)
            {
                NodeEditorWindow.Open(_treeGraphOnRuntime);
            }
            else
            {
                NodeEditorWindow.Open(TreeGraph);
            }
        }

#endif
    }
}