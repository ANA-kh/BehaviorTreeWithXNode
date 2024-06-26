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
        
        [SerializeField]
        private AbortType abortType = AbortType.None;
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

        public override void AbortRight(int childIndex)
        {
            if (childIndex <= currentChild)
            {
                state = State.Inactive;
                OnStop();
                for (var i = childIndex + 1; i < children.Count; i++)
                {
                    children[i].Abort();
                }
                parent?.AbortRight(indexInParent);
                currentChild = childIndex;
            }
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
            //TODO Abort会调用OnStop,OnStop中会清理数据,会把currentChild设为0。这里依赖了执行顺寻,后续考虑优化
            currentChild = childIndex;
        }
    }
    
    
    /// <summary>
    /// Self: 当前分支
    /// LowerPriority: 右侧所有分支
    /// </summary>
    public enum AbortType { None, LowerPriority, Self, Both }
}