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
        public List<BehaviourNode> Children
        {
            get => children;
            protected set => children = value;
        }

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
            Children.Add(child);
            child.SetParent(this, Children.Count - 1);
        }
        
        public void RemoveChild(BehaviourNode child)
        {
            Children.Remove(child);
        }
        
        public void ClearChildren()
        {
            Children.Clear();
        }

        public override void Abort()
        {
            state = State.Inactive;
            OnStop();
            foreach (var child in Children)
            {
                child.Abort();
            }
        }

        public override void AbortRight(int index)
        {
            for (int i = index + 1; i < Children.Count; i++)
            {
                Children[i].Abort();
            }
            parent?.AbortRight(indexInParent);
        }

        internal override void OnObserverBegin()
        {
            foreach (var child in Children)
            {
                child.OnObserverBegin();
            }
        }

        internal override void OnObserverEnd()
        {
            foreach (var child in Children)
            {
                child.OnObserverEnd();
            }
        }
        
        internal override void OnConditionalAbort(int childIndex)
        {
            if (abortType == AbortType.Self)
            {
                Abort();
            }
            else if (abortType == AbortType.LowerPriority)
            {
                Abort();
                parent?.AbortRight(indexInParent);
            }
            currentChild = childIndex;
        }
    }
    
    
    /// <summary>
    /// Self: 当前分支
    /// LowerPriority: 右侧所有分支
    /// </summary>
    public enum AbortType { None, LowerPriority, Self, Both }
}