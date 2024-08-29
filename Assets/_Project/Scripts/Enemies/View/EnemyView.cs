using Project.Configs.Enemies;
using Project.General.View;
using Project.Spawner;
using UnityEngine;

namespace Project.Enemies.View
{
    public class EnemyView : ShipView
    {
        [SerializeField] private MeshFilter _shipMesh;
        [SerializeField] private MeshFilter _sailMesh;

        private VfxSpawner _vfxSpawner;

        public void Initialize(EnemyShipView view, VfxSpawner vfxSpawner)
        {
            _shipMesh.mesh = view.ShipMesh;
            _sailMesh.mesh = view.SailsMesh;
            _vfxSpawner = vfxSpawner;
            transform.localPosition = transform.localPosition + view.GetViewOffset();

            SetOriginLocalPosition(transform.localPosition);
            InitializeShipSwinger(view.GetWaterlineOffset());
        }

        public void Shoot(Vector3 targetPosition)
        {
            _vfxSpawner.SpawnCannonSmoke(transform.position, targetPosition);
        }

        public void TakeDamage()
        {
            _vfxSpawner.SpawnExplosion(transform.position, transform);
        }

    }
}