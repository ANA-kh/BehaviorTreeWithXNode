using UnityEngine;

namespace BT
{
    [System.Serializable]
    public abstract class DecoratorNode : BehaviourNode
    {
        protected BehaviourNode child;
        public BehaviourNode Child
        {
            get => child;
            protected set => child = value;
        }

        public void AddChild(BehaviourNode child)
        {
            this.Child = child;
            child.SetParent(this, 0);
        }

        public override void Abort()
        {
            state = State.Inactive;
            OnStop();
            Child.Abort();
        }

        public override void AbortRight(int index)
        {
            parent?.AbortRight(indexInParent);
        }
    }
}