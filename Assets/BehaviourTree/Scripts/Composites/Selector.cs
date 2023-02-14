namespace BT.Composites
{
    [System.Serializable]
    public class Selector : CompositeNode
    {
        protected override void OnStart()
        {
            base.OnStart();
            currentChild = 0;
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