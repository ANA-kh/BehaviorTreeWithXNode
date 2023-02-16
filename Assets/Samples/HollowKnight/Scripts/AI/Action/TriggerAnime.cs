namespace Core.BT
{
    public class TriggerAnime : EnemyAction
    {
        public string TriggerName;
        protected override State OnUpdate()
        {
            animator.SetTrigger(TriggerName);
            return State.Success;
        }
    }
}