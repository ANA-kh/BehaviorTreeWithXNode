namespace BT
{
    [System.Serializable]
    public class Node
    {
        public enum State
        {
            Inactive,
            Running,
            Failure,
            Success
        }
        
        public State state = State.Inactive;

        public State Update()
        {
            if (state != State.Running) OnStart();
            state = OnUpdate();
            if (state != State.Running) OnStop();
            
            return state;
        }
        
        public void Abort()
        {
            OnAbort();
        }
        
        protected virtual void OnStart() { }
        protected virtual void OnStop() { }
        protected virtual State OnUpdate() { return State.Running; }
        protected virtual void OnAbort() { }
    }
}