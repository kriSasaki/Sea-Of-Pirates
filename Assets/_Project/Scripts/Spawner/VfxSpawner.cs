using UnityEngine;

namespace Project.Spawner
{
    public class VfxSpawner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _cannonSmokePrefab;
        [SerializeField] private ParticleSystem _explosionPrefab;

        public void SpawnCannonSmoke(Collider shooterCollider, Vector3 targetPosition)
        {
            Vector3 spawnPosition = shooterCollider.ClosestPoint(targetPosition);
            SpawnCannonSmoke(spawnPosition, targetPosition);
        }

        public void SpawnCannonSmoke(Vector3 from, Vector3 towards)
        {
            Instantiate(_cannonSmokePrefab, from, Quaternion.LookRotation(towards - from));
        }

        public void SpawnExplosion(Vector3 atPosition, Transform parent = null)
        {
            Instantiate(_explosionPrefab, atPosition, Quaternion.identity, parent);
        }
    }
}