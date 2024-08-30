using Project.General.View;
using Project.Interfaces.Audio;
using Project.Spawner;
using UnityEngine;

namespace Project.Enemies.View
{
    public class EnemyView : ShipView
    {
        [SerializeField] private MeshFilter _shipMesh;
        [SerializeField] private MeshFilter _sailMesh;
        [SerializeField] private AudioClip _shootSound;
        [SerializeField] private AudioClip _hitSound;


        private VfxSpawner _vfxSpawner;
        private IAudioService _audioService;

        public Bounds ShipBounds => _shipMesh.sharedMesh.bounds;
        public void Initialize(VfxSpawner vfxSpawner, IAudioService audioService)
        {
            _vfxSpawner = vfxSpawner;
            _audioService = audioService;
        }

        public void Shoot(Vector3 targetPosition)
        {
            _vfxSpawner.SpawnCannonSmoke(transform.position, targetPosition);
            _audioService.PlaySound(_shootSound);
        }

        public void TakeDamage()
        {
            _vfxSpawner.SpawnExplosion(transform.position, transform);
            _audioService.PlaySound(_hitSound);
        }
    }
}