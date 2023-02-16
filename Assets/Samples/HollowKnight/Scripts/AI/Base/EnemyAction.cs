using BT;
using Core.Character;
using Core.Combat;
using UnityEngine;

namespace Core.BT
{
    public abstract class EnemyAction : ActionNode
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected Destructable destructable;
        protected PlayerController player;
        
        public override void Init(BehaviourTree tree)
        {
            base.Init(tree);
            body = tree.Agent.GetComponent<Rigidbody2D>();
            player = PlayerController.Instance;
            destructable = tree.Agent.GetComponent<Destructable>();
            animator = tree.Agent.GetComponentInChildren<Animator>();
        }
    }
}