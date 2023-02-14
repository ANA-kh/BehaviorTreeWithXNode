using System;
using UnityEngine;

namespace BT
{
    [System.Serializable]
    public class BehaviourNode
    {
        private BehaviourTree tree;
        protected int indexInParent;
        protected BehaviourNode parent;
        
        protected Blackboard Blackboard
        {
            get { return tree.Blackboard; }
        }
        public enum State
        {
            Inactive,
            Running,
            Failure,
            Success
        }
        
        [HideInInspector]
        public State state = State.Inactive;

        public State Update()
        {
            if (state != State.Running) OnStart();
            state = OnUpdate();
            if (state != State.Running) OnStop();
            
            return state;
        }
        
        public void Abort()
        {
            OnAbort();
        }
        public void SetParent(BehaviourNode parent, int indexInParent)
        {
            this.parent = parent;
            this.indexInParent = indexInParent;
        }
        
        protected virtual void OnStart() { }
        protected virtual void OnStop() { }
        protected virtual State OnUpdate() { return State.Running; }

        protected virtual void OnAbort()
        {
            state = State.Inactive;
            OnStop();
        }

        internal virtual void OnObserverBegin() { }
        
        internal virtual void OnObserverEnd() { }
        internal virtual void OnConditionalAbort(int childIndex) { }

        public virtual void AbortRight(int index) { }
    }
}