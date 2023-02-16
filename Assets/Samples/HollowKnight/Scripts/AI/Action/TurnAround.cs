namespace Core.BT
{
    public class TurnAround : EnemyAction
    {
        protected override State OnUpdate()
        {
            var scale = Agent.transform.localScale;
            scale.x *= -1;
            Agent.transform.localScale = scale;
            return State.Success;
        }
    }
}