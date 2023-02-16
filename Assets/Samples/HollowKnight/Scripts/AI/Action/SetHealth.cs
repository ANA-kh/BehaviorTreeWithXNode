namespace Core.BT
{
    public class SetHealth : EnemyAction
    {
        public int Health;

        protected override State OnUpdate()
        {
            destructable.CurrentHealth = Health;
            return State.Success;
        }
    }
}