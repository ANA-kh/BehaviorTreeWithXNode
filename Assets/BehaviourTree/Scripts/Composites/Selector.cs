namespace BT.Composites
{
    [System.Serializable]
    public class Selector : CompositeNode
    {
        protected override void OnStop()
        {
            base.OnStop();
            currentChild = 0;
        }

        protected override State OnUpdate()
        {
            for (int i = currentChild; i < Children.Count; ++i)
            {
                currentChild = i;
                var child = Children[currentChild];
                
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