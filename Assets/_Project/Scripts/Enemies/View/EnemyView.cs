using Project.Configs.Enemies;
using Project.General.View;
using UnityEngine;

namespace Project.Enemies.View
{
    public class EnemyView : ShipView
    {
        [SerializeField] private MeshFilter _shipMesh;
        [SerializeField] private MeshFilter _sailMesh;

        public void Initialize(EnemyShipView view)
        {
            _shipMesh.mesh = view.ShipMesh;
            _sailMesh.mesh = view.SailsMesh;
            transform.localPosition = transform.localPosition + view.GetViewOffset();

            SetOriginLocalPosition(transform.localPosition);
            InitializeShipSwinger(view.GetViewOffset());
        }
    }
}