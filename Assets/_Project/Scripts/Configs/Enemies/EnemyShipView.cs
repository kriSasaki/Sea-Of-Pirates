using Project.Utils.Extensions;
using UnityEngine;

namespace Project.Configs.Enemies
{
    [CreateAssetMenu(fileName = "ShipView", menuName = "Configs/Enemies/ShipView")]
    public class EnemyShipView : ScriptableObject
    {
        [SerializeField, Range(0.01f, 0.08f)] private float _verticalOffsetPercent = 0.05f;
        [SerializeField, Range(0.05f, 0.2f)] private float _waterlineOffsetPercent = 0.09f;

        [field: SerializeField] public Mesh ShipMesh { get; private set; }
        [field: SerializeField] public Mesh SailsMesh { get; private set; }

        public Vector3 GetViewOffset()
        {
            float verticalOffset = ShipMesh.bounds.size.y * -_verticalOffsetPercent;

            return Vector3.zero.WithY(verticalOffset);
        }

        public Vector3 GetWaterlineOffset()
        {
            float verticalOffset = ShipMesh.bounds.size.y * -_waterlineOffsetPercent;

            return Vector3.zero.WithY(verticalOffset);
        }

        public void SetShipColliderSize(BoxCollider boxCollider)
        {
            boxCollider.size = ShipMesh.bounds.size;
            boxCollider.center = boxCollider.center.WithY(boxCollider.size.y / 2);
        }
    }
}