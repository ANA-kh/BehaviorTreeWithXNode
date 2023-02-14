using System;

namespace BT
{
    [System.Serializable]
    public abstract class ConditionNode : BehaviourNode
    {
        public bool IsObserving { get; protected set; } = false;
        public abstract bool Condition(); 

        protected void Evaluate()
        {
            bool conditionResult = Condition();

            if (conditionResult)
            {
                parent.OnConditionalAbort(indexInParent);
            }
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}