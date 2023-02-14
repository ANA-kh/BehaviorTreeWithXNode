using System;
using UnityEngine;

namespace BT
{
    [System.Serializable]
    public class BehaviourTree 
    {
        // public BehaviourNode RootNode
        // {
        //     get => _rootNode;
        //     set => _rootNode = value;
        // }
        public GameObject Agent { get; set; } 
        [SerializeField]
        public Blackboard Blackboard = new Blackboard();
        [HideInInspector]
        public BehaviourNode.State TreeState = BehaviourNode.State.Inactive;
        [NonSerialized]//避免循环引用,导致序列化失败(unity序列化层数限制)
        private BehaviourNode _rootNode;

        public BehaviourTree() { } 

        public BehaviourTree(BehaviourNode root)
        {
            _rootNode = root;
        }

        public BehaviourNode.State Update()
        {
            return _rootNode.Update(); 
        }
        
        public void SetRootNode(BehaviourNode root)
        {
            _rootNode = root;
        }

        public void Init(GameObject agent)
        {
            Agent = agent;
            Blackboard.Init();
            TreeState = BehaviourNode.State.Running;
        }
    }
}