using System;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    [System.Serializable]
    public class BehaviourTree
    {
        public Node RootNode { get; set; }
        public GameObject Agent { get; set; }
        public Blackboard Blackboard = new Blackboard();
        [HideInInspector]
        public Node.State TreeState = Node.State.Inactive;

        public BehaviourTree() { }

        public BehaviourTree(Node root)
        {
            RootNode = root;
        }

        public Node.State Update()
        {
            return RootNode.Update();
        }
    }
}