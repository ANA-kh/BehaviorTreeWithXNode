namespace Core.BT
{
    public class GotoNextStage : EnemyAction
    {
        //public int CurrentStage;
        protected override State OnUpdate()
        {
            //TODO 通用完善 var stage = Blackboard.Get<int>("CurrentStage");
            if (Blackboard.TryGet( "CurStage", out int stage)) 
                Blackboard.Set("CurStage", stage + 1);
            return State.Success;
        }
    }
}