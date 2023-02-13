namespace BT.Composites
{
    [System.Serializable]
    public class Selector : CompositeNode
    {
        protected int currentChild = 0;
        
        protected override void OnStart()
        {
            currentChild = 0;
        }
        
        protected override void OnStop()
        {
        }
        
        protected override State OnUpdate()
        {
            for (int i = currentChild; i < children.Count; ++i)
            {
                currentChild = i;
                var child = children[currentChild];
                
                switch (child.Update())
                {
                    case State.Running:
                        return State.Running;
                    case State.Success:
                        return State.Success;
                    case State.Failure:
                        continue;
                }
            }
            
            return State.Failure;
        }
    }
}