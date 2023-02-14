namespace BT.Composites
{
    [System.Serializable]
    public class Sequencer : CompositeNode
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
                    case State.Failure:
                        return State.Failure;
                    case State.Success:
                        continue;
                }
            }
            
            return State.Success;
        }
    }
}