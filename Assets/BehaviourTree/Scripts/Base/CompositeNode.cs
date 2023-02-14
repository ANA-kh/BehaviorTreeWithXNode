using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    [System.Serializable]
    public abstract class CompositeNode : BehaviourNode
    {
        [HideInInspector]
        [SerializeReference]
        protected List<BehaviourNode> children = new List<BehaviourNode>();
        
        public AbortType abortType = AbortType.None;
        protected int currentChild = 0; 

        protected override void OnStart()
        {
            if (abortType != AbortType.None)
            {
                OnObserverBegin();
            }
        }

        protected override void OnStop()
        {
            if (abortType == AbortType.Self)
            {
                OnObserverEnd();
            }
        }
        
        public void AddChild(BehaviourNode child)
        {
            children.Add(child);
            child.SetParent(this, children.Count - 1);
        }
        
        public void RemoveChild(BehaviourNode child)
        {
            children.Remove(child);
        }
        
        public void ClearChildren()
        {
            children.Clear();
        }

        public override void AbortRight(int index)
        {
            for (int i = index + 1; i < children.Count; i++)
            {
                children[i].Abort();
            }
        }

        internal override void OnObserverBegin()
        {
            foreach (var child in children)
            {
                child.OnObserverBegin();
            }
        }

        internal override void OnObserverEnd()
        {
            foreach (var child in children)
            {
                child.OnObserverEnd();
            }
        }
        
        internal override void OnConditionalAbort(int childIndex)
        {
            currentChild = childIndex;
            state = State.Inactive;
            if (abortType == AbortType.Self)
            {
                AbortCurrentBranch();
            }
            else if (abortType == AbortType.LowerPriority)
            {
                AbortLowerPriorityBranch();
            }
        }

        private void AbortLowerPriorityBranch()
        {
            parent.AbortRight(indexInParent);
            AbortCurrentBranch();
        }

        private void AbortCurrentBranch()
        {
            foreach (var child in children)
            {
                child.Abort();
            }
        }
    }
    
    
    /// <summary>
    /// Self: 当前分支
    /// LowerPriority: 右侧所有分支
    /// </summary>
    public enum AbortType { None, LowerPriority, Self, Both }
}