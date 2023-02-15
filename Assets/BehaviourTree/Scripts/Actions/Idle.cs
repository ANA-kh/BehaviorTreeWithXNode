namespace BT.Actions
{
    public class Idle : ActionNode
    {
        protected override State OnUpdate()
        {
            return State.Running;
        }
    }
}