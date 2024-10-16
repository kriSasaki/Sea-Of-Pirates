using System;
using Scripts.Interactables;
using Scripts.Interfaces.Data;
using Scripts.Systems.Data;
using UnityEngine.SceneManagement;

namespace Scripts.Players.Logic
{
    public class PlayerSpawner : IDisposable
    {
        private readonly PirateBay _pirateBay;
        private readonly Player _player;
        private readonly LevelData _levelData;

        public PlayerSpawner(
            PirateBay pirateBay,
            Player player,
            ILevelDataService levelDataService)
        {
            _pirateBay = pirateBay;
            _player = player;
            _levelData = levelDataService.GetLevelData(SceneManager.GetActiveScene().name);
        }

        public void Initialize()
        {
            if (_levelData.IsReachedPirateBay)
                _player.SetPosition(_pirateBay.PlayerRessurectPoint.position);
            else
                _pirateBay.ReachedByPlayer += OnReachedPirateBay;
        }

        public void Dispose()
        {
            _pirateBay.ReachedByPlayer -= OnReachedPirateBay;
        }

        private void OnReachedPirateBay()
        {
            _levelData.ReachPirateBay();
            _pirateBay.ReachedByPlayer -= OnReachedPirateBay;
        }
    }
}