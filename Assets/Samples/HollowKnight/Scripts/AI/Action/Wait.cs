using DG.Tweening;
using UnityEngine;

namespace Core.BT
{
    public class Wait : EnemyAction
    {
        public float WaitTime;
        
        //private Tween _waitTween;
        
        private float _startTime;
        
        protected override void OnStart()
        {
            _startTime = Time.time;
        }

        protected override State OnUpdate()
        {
            return Time.time - _startTime > WaitTime ? State.Success : State.Running;
        }
    }
}