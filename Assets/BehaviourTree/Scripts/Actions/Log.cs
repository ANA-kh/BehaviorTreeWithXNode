using UnityEngine;

namespace BT.Actions
{
    public class Log : ActionNode
    {
        public string message;

        protected override State OnUpdate()
        {
            Debug.Log(message);
            return State.Success;
        }
    }
}