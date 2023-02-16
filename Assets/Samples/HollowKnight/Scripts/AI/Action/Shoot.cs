using Core.Character;
using Core.Combat;
using UnityEngine;

namespace Core.BT
{
    public class Shoot : EnemyAction
    {
        public Weapon weapon;
        public bool ShakeCamera;

        protected override void OnStart()
        {
            //TODO 让transform可在inspector中设置
            weapon.weaponTransform = Agent.transform.Find("WeaponTransform");
        }

        protected override State OnUpdate()
        {
            var projectile = Object.Instantiate(weapon.projectilePrefab,weapon.weaponTransform.position ,
                Quaternion.identity);
            projectile.Shooter = Agent.gameObject;
                
            var force = new Vector2(weapon.horizontalForce * Agent.transform.localScale.x,weapon.verticalForce);
            projectile.SetForce(force);

            if (ShakeCamera)
                CameraController.Instance.ShakeCamera(0.5f);
            return State.Success;
        }
    }
}