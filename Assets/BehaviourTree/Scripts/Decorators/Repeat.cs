using System;

namespace BT.Decorators
{
    [System.Serializable]
    public class Repeat : DecoratorNode
    {
        public bool RepeatForever = false;
        public int RepeatCount = 1;
        private int _currentCount = 0;

        protected override void OnStart()
        {
            _currentCount = 0;
        }

        protected override State OnUpdate()
        {
            if (RepeatForever || _currentCount < RepeatCount)
            {
                switch (Child.Update())
                {
                    case State.Failure:
                    case State.Success:
                        _currentCount++;
                        break;
                }
                return State.Running;
            }
            else
            {
                return State.Success;
            }
        }

        protected override void OnStop()
        {
            _currentCount = 0;
        }
    }
}