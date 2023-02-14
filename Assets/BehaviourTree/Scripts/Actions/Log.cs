using UnityEngine;

namespace BT.Actions
{
    [System.Serializable]
    public class Log : ActionNode
    {
        public int Count;
        public string message;
        private int _curCount;

        protected override void OnStart()
        {
            _curCount = 0;
        }

        protected override State OnUpdate()
        {
            if (_curCount >= Count)
            {
                return State.Success;
            }
            Debug.Log(message);
            _curCount++;
            return State.Running;
        }
    }
}