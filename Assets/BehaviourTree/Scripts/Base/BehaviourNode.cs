using System;
using UnityEngine;

namespace BT
{
    [System.Serializable]
    public abstract class BehaviourNode
    {
        private BehaviourTree tree;
        protected int indexInParent;
        protected BehaviourNode parent;

        protected Blackboard Blackboard
        {
            get { return tree.Blackboard; }
        }
        protected GameObject Agent
        {
            get { return tree.Agent; }
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
            if (state != State.Running)
            {
                OnStart();
                return state = State.Running;
            }

            state = OnUpdate();
            if (state != State.Running) OnStop();

            return state;
        }

        public virtual void Abort()
        {
            state = State.Inactive;
            OnStop();
        }

        public void SetParent(BehaviourNode parent, int indexInParent)
        {
            this.parent = parent;
            this.indexInParent = indexInParent;
        }

        public virtual void Init(BehaviourTree tree)
        {
            this.tree = tree;
        }

        protected virtual void OnStart() { }
        protected virtual void OnStop() { }

        protected virtual State OnUpdate()
        {
            return State.Running;
        }

        internal virtual void OnObserverBegin() { }

        internal virtual void OnObserverEnd() { }
        internal virtual void OnConditionalAbort(int childIndex) { }

        public virtual void AbortRight(int childIndex) { }
    }
}