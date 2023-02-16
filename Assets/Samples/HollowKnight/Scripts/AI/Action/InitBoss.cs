using Core.UI;

namespace Core.BT
{
    public class InitBoss : EnemyAction
    {
        public string BossName;
        
        protected override State OnUpdate()
        {
            GuiManager.Instance.ShowBossName(BossName);
            Blackboard.Set("Health", destructable.CurrentHealth);
            destructable.OnHealthChanged += (health) => { Blackboard.Set("Health", health); };
            return State.Success;
        }
    }
}