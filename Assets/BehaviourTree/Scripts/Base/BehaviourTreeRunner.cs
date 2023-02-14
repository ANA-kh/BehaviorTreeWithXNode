using System;
using UnityEngine;

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
            //_treeGraphOnRuntime = Instantiate(TreeGraph);   wrong
            _treeGraphOnRuntime = (BehaviourTreeGraph)TreeGraph.Copy();
            //_treeGraphOnRuntime = ScriptableObject.CreateInstance<BehaviourTreeGraph>();
            BTree = _treeGraphOnRuntime.Build();
            BTree.Agent = gameObject;
            BTree.TreeState = BehaviourNode.State.Running;
        }

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

        private void OnDestroy()
        {
            BTree.TreeState = BehaviourNode.State.Inactive;
        }
    }
}