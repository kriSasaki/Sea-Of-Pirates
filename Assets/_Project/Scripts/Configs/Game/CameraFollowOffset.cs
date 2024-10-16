using UnityEngine;

namespace Scripts.Configs.Game
{
    [CreateAssetMenu(fileName = "CameraFollowOffset", menuName = "Configs/CameraFollow")]
    public class CameraFollowOffset : ScriptableObject
    {
        [SerializeField, Range(-40f, 40f)] private float _x = 0f;
        [SerializeField, Range(30f, 60f)] private float _y = 40f;
        [SerializeField, Range(-60f, 40f)] private float _z = -40f;

        public Vector3 Value => new Vector3(_x, _y, _z);
    }
}