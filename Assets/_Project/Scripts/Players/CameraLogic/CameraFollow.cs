using UnityEngine;

namespace Project.Players.CamaraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _following;
        [SerializeField] private float RotationAngleX;
        [SerializeField] private float Distance;
        [SerializeField] private float OffsetY;

        private void LateUpdate()
        {
            if (_following == null)
            {
                return;
            }
            Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);

            Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += OffsetY;

            return followingPosition;
        }
    }
}