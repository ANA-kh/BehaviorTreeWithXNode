namespace Core.BT
{
    public class FreezeTime : EnemyAction
    {
        public float Duration = 0.1f;
        protected override State OnUpdate()
        {
            GameManager.Instance.FreezeTime(Duration);
            return State.Success;
        }
    }
}