using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    [System.Serializable]
    public abstract class CompositeNode : Node
    {
        [HideInInspector]
        [SerializeReference]
        public List<Node> children = new List<Node>();
    }
}