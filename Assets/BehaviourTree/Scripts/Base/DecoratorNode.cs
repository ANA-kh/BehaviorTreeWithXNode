using UnityEngine;

namespace BT
{
    [System.Serializable]
    public abstract class DecoratorNode : BehaviourNode
    {
        protected BehaviourNode child;
        
        public void AddChild(BehaviourNode child)
        {
            this.child = child;
            child.SetParent(this, 0);
        }
    }
}