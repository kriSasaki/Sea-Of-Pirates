using UnityEngine;

namespace Project.Enemies
{
    public class EnemyWandering : MonoBehaviour
    {
        private Vector3 _center;
        
        public void StartMoving(Vector3 center)
        {
            _center = center;
            
        }
    }
}