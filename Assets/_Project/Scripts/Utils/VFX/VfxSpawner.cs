using UnityEngine;

namespace Project.Utils.VFX
{
    public class VfxSpawner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _cannonSmokePrefab;
        [SerializeField] private ParticleSystem _explosionPrefab;

        public void SpawnCannonSmoke(Collider shooterCollider, Vector3 targetPosition)
        {
            Vector3 spawnPosition = shooterCollider.ClosestPoint(targetPosition);
            Instantiate(_cannonSmokePrefab, spawnPosition, Quaternion.LookRotation(targetPosition - spawnPosition));
        }

        public void SpawnExplosion(Vector3 atPosition)
        {
            Instantiate(_explosionPrefab, atPosition, Quaternion.identity);
        }
    }
}