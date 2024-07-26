using UnityEngine;

namespace Project.Spawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _spawnPoint;

        public void SpawnPlayer()
        {
            Instantiate(_playerPrefab, _spawnPoint.transform.position, Quaternion.identity);
        }
    }
}