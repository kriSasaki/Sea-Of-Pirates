using UnityEngine;

namespace Project.Spawner
{
    public class VfxSpawner : MonoBehaviour
    {
        private readonly Vector3 ExplotionOffset = Vector3.up;

        [SerializeField] private ParticleSystem _cannonSmokePrefab;
        [SerializeField] private ParticleSystem _explosionPrefab;
        [SerializeField] private DamagePopup _damagePopupPrefab;

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
            Instantiate(_explosionPrefab, atPosition + ExplotionOffset, Quaternion.identity, parent);
        }

        public void ShowDamage(Vector3 atposition, int damage)
        {
            Quaternion rotation = Camera.main.transform.rotation;
            var damagePopup = Instantiate(_damagePopupPrefab, atposition, rotation);
            damagePopup.Initialize(damage);
        }
    }
}