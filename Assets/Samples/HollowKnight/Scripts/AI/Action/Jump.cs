using Core.Character;
using DG.Tweening;
using UnityEngine;

namespace Core.BT
{
    public class Jump : EnemyAction
    {
        public float horizontalForce = 5.0f;
        public float jumpForce = 10.0f;

        public float buildupTime;
        public float jumpTime;

        public string animationTriggerName;
        public bool shakeCameraOnLanding;

        private bool hasLanded;

        private Tween buildupTween;
        private Tween jumpTween;
        
        protected override void OnStart()
        {
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
            animator.SetTrigger(animationTriggerName);
        }
        
        private void StartJump()
        {
            var direction = player.transform.position.x < Agent.transform.position.x ? -1 : 1;
            body.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);

            jumpTween = DOVirtual.DelayedCall(jumpTime, () =>
            {
                hasLanded = true;
                if (shakeCameraOnLanding)
                    CameraController.Instance.ShakeCamera(0.5f);
            }, false);
        }
        
        protected override State OnUpdate()
        {
            return hasLanded ? State.Success : State.Running;
        }

        protected override void OnStop()
        {
            buildupTween?.Kill();
            jumpTween?.Kill();
            hasLanded = false;
        }
    }
}