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

        private void OnEnable()   
        {
            //_treeGraphOnRuntime = Instantiate(TreeGraph);   wrong
            _treeGraphOnRuntime = (BehaviourTreeGraph)TreeGraph.Copy();
            //_treeGraphOnRuntime = ScriptableObject.CreateInstance<BehaviourTreeGraph>();
            _behaviourTree = _treeGraphOnRuntime.Build();
            _behaviourTree.Agent = gameObject;
            _behaviourTree.TreeState = Node.State.Running;
        }

        private void Update()
        {
            if (_behaviourTree.TreeState == Node.State.Running)
            {
                _timer += Time.deltaTime;
                if (_timer > TickTime)
                {
                    _timer -= TickTime;
                    _behaviourTree.Update();
                }
            }
        }

        private void OnDestroy()
        {
            _behaviourTree.TreeState = Node.State.Inactive;
        }
    }
}