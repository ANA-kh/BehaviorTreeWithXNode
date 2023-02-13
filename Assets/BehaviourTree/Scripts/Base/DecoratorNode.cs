using UnityEngine;

namespace BT
{
    [System.Serializable]
    public abstract class DecoratorNode : Node
    {
        [SerializeReference]
        [HideInInspector]
        public Node child;
    }
}