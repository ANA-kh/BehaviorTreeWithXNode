using Core.Combat.Projectiles;
using DG.Tweening;
using UnityEngine;

namespace Core.BT
{
    public class SpawnFallingRocks : EnemyAction
    {
        public Collider2D spawnAreaCollider;
        public AbstractProjectile rockPrefab;
        public int spawnCount = 4;
        public float spawnInterval = 0.3f;

        protected override void OnStart()
        {
            spawnAreaCollider = GameObject.Find("RockSpawnArae")?.GetComponent<Collider2D>();
        }

        protected override State OnUpdate()
        {
            var sequence = DOTween.Sequence();
            for (int i = 0; i < spawnCount; i++)
            {
                sequence.AppendCallback(SpawnRock);
                sequence.AppendInterval(spawnInterval);
            }

            return State.Success;
        }
        
        private void SpawnRock()
        {
            var randomX = Random.Range(spawnAreaCollider.bounds.min.x, spawnAreaCollider.bounds.max.x);
            var rock = Object.Instantiate(rockPrefab, new Vector3(randomX, spawnAreaCollider.bounds.min.y),
                Quaternion.identity);
            rock.SetForce(Vector2.zero);
        }
    }
}