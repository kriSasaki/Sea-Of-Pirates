using Scripts.Enemies;
using Scripts.Interfaces.Audio;
using Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Scripts.Spawner
{
    public class VfxSpawner : MonoBehaviour
    {
        private readonly Vector3 ExplotionOffset = Vector3.up;

        [SerializeField] private ParticleSystem _cannonSmokePrefab;
        [SerializeField] private ParticleSystem _explosionPrefab;
        [SerializeField] private DamagePopup _damagePopupPrefab;
        [SerializeField] private Projectile _projectilePrefab;

        private VFXPool<ParticleSystem> _smokePool;
        private VFXPool<ParticleSystem> _explosionPool;
        private VFXPool<DamagePopup> _damagePopupPool;
        private VFXPool<Projectile> _projectilePool;

        private IAudioService _audioService;

        public void SpawnCannonSmoke(Collider shooterCollider, Vector3 targetPosition)
        {
            Vector3 spawnPosition = shooterCollider.ClosestPoint(targetPosition);
            SpawnCannonSmoke(spawnPosition, targetPosition);
        }

        public void SpawnCannonSmoke(Vector3 from, Vector3 towards)
        {
            var smoke = _smokePool.Get();
            smoke.transform.SetPositionAndRotation(from, Quaternion.LookRotation(towards - from));
            smoke.Play();
        }

        public void SpawnExplosion(Vector3 atPosition, Transform parent = null)
        {
            var explotion = _explosionPool.Get();
            explotion.transform.SetPositionAndRotation(atPosition + ExplotionOffset, Quaternion.identity);
            explotion.transform.SetParent(parent);
            explotion.Play();
        }

        public void SpawnDamagePopup(Vector3 atPosition, int damage)
        {
            Quaternion rotation = Camera.main.transform.rotation;
            DamagePopup damagePopup = _damagePopupPool.Get();
            damagePopup.transform.SetPositionAndRotation(atPosition, rotation);
            damagePopup.Initialize(damage);
        }

        public Projectile SpawnProjectile(Vector3 atPosition)
        {
            var projectile = _projectilePool.Get();
            projectile.Initialize(_audioService);
            projectile.transform.SetPositionAndRotation(atPosition, Quaternion.identity);

            return projectile;
        }

        [Inject]
        private void Construct(IAudioService audioService)
        {
            _smokePool = new VFXPool<ParticleSystem>(_cannonSmokePrefab);
            _explosionPool = new VFXPool<ParticleSystem>(_explosionPrefab);
            _damagePopupPool = new VFXPool<DamagePopup>(_damagePopupPrefab);
            _projectilePool = new VFXPool<Projectile>(_projectilePrefab);
            _audioService = audioService;
        }
    }
}