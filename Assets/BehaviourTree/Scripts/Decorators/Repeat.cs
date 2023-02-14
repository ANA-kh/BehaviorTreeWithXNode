using System;

namespace BT.Decorators
{
    [System.Serializable]
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

                switch (child.Update())
                {
                    case State.Failure:
                    case State.Success:
                        currentCount++;
                        break;
                }
                return State.Running;
            }
            else
            {
                return State.Success;
            }
        }
    }
}