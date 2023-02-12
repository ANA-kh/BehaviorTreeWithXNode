namespace BT.Decorators
{
    public class Repeat : DecoratorNode
    {
        public int repeatCount = 1;
        private int currentCount = 0;

        protected override void OnStart()
        {
            currentCount = 0;
        }

        protected override State OnUpdate()
        {
            if (currentCount < repeatCount)
            {
                currentCount++;
                child.Update();
                return State.Running;
            }
            else
            {
                return State.Success;
            }
        }
    }
}