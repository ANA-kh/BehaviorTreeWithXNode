namespace BT
{
    [System.Serializable]
    public class CheckState : ConditionNode
    {
        public int CurStage;
        public override bool Condition()
        {
            var state = Blackboard.Get<int>("CurStage");
            return state == CurStage;
        }

        internal override void OnObserverBegin()
        {
            if (!IsObserving)
            {
                IsObserving = true;
                Blackboard.AddObserver("CurStage", Evaluate);
            }
        }

        internal override void OnObserverEnd()
        {
            if (IsObserving)
            {
                IsObserving = false;
                Blackboard.RemoveObserver("CurStage", Evaluate);   
            }
        }
    }
}