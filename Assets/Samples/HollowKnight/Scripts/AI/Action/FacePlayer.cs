using BT;
using UnityEngine.UIElements;

namespace Core.BT
{
    public class FacePlayer : EnemyAction
    {
        private float _baseScaleX;

        public override void Init(BehaviourTree tree)
        {
            base.Init(tree);
            _baseScaleX = Agent.transform.localScale.x;
        }

        protected override State OnUpdate()
        {
            var scale = Agent.transform.localScale;
            scale.x = Agent.transform.position.x > player.transform.position.x ? -_baseScaleX : _baseScaleX;
            Agent.transform.localScale = scale;

            return State.Success;
        }
    }
}