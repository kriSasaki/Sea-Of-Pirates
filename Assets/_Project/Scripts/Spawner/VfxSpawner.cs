using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.Spawner
{
    public class VfxSpawner : MonoBehaviour
    {
        private readonly Vector3 ExplotionOffset = Vector3.up;

        [SerializeField] private ParticleSystem _cannonSmokePrefab;
        [SerializeField] private ParticleSystem _explosionPrefab;
        [SerializeField] private DamagePopup _damagePopupPrefab;

        private VFXPool<ParticleSystem> _smokePool;
        private VFXPool<ParticleSystem> _explosionPool;
        private VFXPool<DamagePopup> _damagePopupPool;

        [Inject]
        private void Construct()
        {
            _smokePool = new VFXPool<ParticleSystem>(_cannonSmokePrefab);
            _explosionPool = new VFXPool<ParticleSystem>(_explosionPrefab);
            _damagePopupPool = new VFXPool<DamagePopup>(_damagePopupPrefab);
        }

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
            //Instantiate(_cannonSmokePrefab, from, Quaternion.LookRotation(towards - from));
        }

        public void SpawnExplosion(Vector3 atPosition, Transform parent = null)
        {
            var explotion =_explosionPool.Get();
            explotion.transform.SetPositionAndRotation(atPosition + ExplotionOffset, Quaternion.identity);
            explotion.transform.SetParent(parent);
            explotion.Play();

            //Instantiate(_explosionPrefab, atPosition + ExplotionOffset, Quaternion.identity, parent);
        }

        public void ShowDamage(Vector3 atposition, int damage)
        {
            Quaternion rotation = Camera.main.transform.rotation;
            var damagePopup = _damagePopupPool.Get();
            damagePopup.transform.SetPositionAndRotation(atposition, rotation);
                //Instantiate(_damagePopupPrefab, atposition, rotation);
            damagePopup.Initialize(damage);
        }
    }

    public class VFXPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly List<T> _pool = new();

        public VFXPool(T prefab)
        {
            _prefab = prefab;
        }

        public T Get()
        {
            var vfx = _pool.FirstOrDefault(v =>  v.gameObject.activeInHierarchy == false);
            if (vfx == null)
            {
                vfx = Create();
            }

            vfx.gameObject.SetActive(true);
            return vfx;
        }

        private T Create()
        {
            var vfx = GameObject.Instantiate(_prefab);
            _pool.Add(vfx);
            return vfx;
        }
    }
}