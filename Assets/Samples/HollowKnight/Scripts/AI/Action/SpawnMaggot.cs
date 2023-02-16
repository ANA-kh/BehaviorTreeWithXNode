using BT;
using Core.Combat;
using UnityEngine;

namespace Core.BT
{
    public class SpawnMaggot : EnemyAction
    {
        public GameObject MaggotPrefab;
        public Transform MaggotTransform;
        public GameObject HazardCollider;
        
        private Destructable maggot;

        public override void Init(BehaviourTree tree)
        {
            base.Init(tree);
            MaggotTransform = Agent.transform.Find("MaggotTransform");
            HazardCollider = Agent.transform.Find("HazardCirle").gameObject;
        }

        protected override void OnStart()
        {
            base.OnStart();
            maggot = Object.Instantiate(MaggotPrefab, MaggotTransform).GetComponent<Destructable>();
            maggot.transform.localPosition = Vector3.zero;
            destructable.Invincible = true;
            HazardCollider.SetActive(false);
        }
        
        protected override State OnUpdate()
        {
            if (maggot.CurrentHealth > 0) return State.Running;
            
            destructable.Invincible = false;
            HazardCollider.SetActive(true);
            return State.Success;
        }
    }
}