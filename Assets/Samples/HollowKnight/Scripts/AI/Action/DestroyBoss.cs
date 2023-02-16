using Core.Character;
using Core.Util;
using DG.Tweening;
using UnityEngine;

namespace Core.BT
{
    public class DestroyBoss : EnemyAction
    {
        public float BleedTime = 2.0f;
        public ParticleSystem BleedEffect;
        public ParticleSystem ExplodeEffect;

        private bool isDestroyed;

        protected override void OnStart()
        {
            EffectManager.Instance.PlayOneShot(BleedEffect,Agent.transform.position);
            DOVirtual.DelayedCall(BleedTime, () =>
            {
                EffectManager.Instance.PlayOneShot(ExplodeEffect, Agent.transform.position);
                CameraController.Instance.ShakeCamera(0.7f);
                isDestroyed = true;
                Object.Destroy(Agent);
            }, false);
        }

        protected override State OnUpdate()
        {
            return isDestroyed ? State.Success : State.Running;
        }
    }
}