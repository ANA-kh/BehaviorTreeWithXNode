namespace BehaviourTree
{
    public class Node
    {
        public enum State
        {
            Running,
            Failure,
            Success
        }
        
        public State state = State.Running;
        public bool started = false;
        
        public State Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state != State.Running)
            {
                OnStop();
                started = false;
            }

            return state;
        }
        
        public void Abort()
        {
            //TODO
        }
        
        protected virtual void OnStart() { }
        protected virtual void OnStop() { }
        protected virtual State OnUpdate() { return State.Running; }
    }
}