using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Players.Logic;
using Project.Spawner;
using Project.Systems.Cameras;
using Project.Systems.Quests;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Infrastructure
{
    public class StartLevelHandler : MonoBehaviour
    {
        private const float MinAlpha = 0f;
        private const float MaxAlpha = 1f;

        [SerializeField] private CanvasGroup _uiCanvasGroup;
        [SerializeField] private float _unfadeCanvasDuration = 0.5f;
        [SerializeField] private GameObject _joystickCanvas;
        [SerializeField] private GameObject _pointerCanvas;

        private List<BaseEnemySpawner> _enemySpawners;
        private QuestEnemyHandler _questEnemyHandler;
        private Player _player;
        private CameraSystem _cameraSystem;

        [Inject]
        public void Construct(
            List<BaseEnemySpawner> enemySpawners,
            QuestEnemyHandler questEnemyHandler,
            Player player,
            CameraSystem cameraSystem)
        {
            _enemySpawners = enemySpawners;
            _questEnemyHandler = questEnemyHandler;
            _player = player;
            _cameraSystem = cameraSystem;
        }

        private async UniTaskVoid Start()
        {
            _player.DisableMove();
            _uiCanvasGroup.alpha = MinAlpha;
            _joystickCanvas.SetActive(false);
            _pointerCanvas.SetActive(false);

            await _cameraSystem.ShowOpeningAsync();

            _player.EnableMove();
            _pointerCanvas.SetActive(true);
            _joystickCanvas.SetActive(true);

            await _uiCanvasGroup.DOFade(MaxAlpha, _unfadeCanvasDuration);

            PrepareSpawners();
            InitializeQuestEnemies();
        }

        private void InitializeQuestEnemies()
        {
            _questEnemyHandler.Initialize();
        }

        private void PrepareSpawners()
        {
            foreach (var spawner in _enemySpawners)
            {
                spawner.Prepare();
            }
        }
    }
}