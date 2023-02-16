using UnityEngine;

namespace Core.BT
{
    public class IsHealthUnder : EnemyConditional
    {
        public string Key = "Health";
        public int HealthThreshold;

        protected override bool Condition()
        {
            if (Blackboard.TryGet(Key, out int health))
            {
                return destructable.CurrentHealth < HealthThreshold;
            }
            else
            {
                Debug.Log("Blackboard does not contain Health" );
                return false;
            }
        }
        
        internal override void OnObserverBegin()
        {
            if (!IsObserving)
            {
                IsObserving = true;
                Blackboard.AddObserver(Key, Evaluate);
            }
        }

        internal override void OnObserverEnd()
        {
            if (IsObserving)
            {
                IsObserving = false;
                Blackboard.RemoveObserver(Key, Evaluate);
            }
        }
    }
}