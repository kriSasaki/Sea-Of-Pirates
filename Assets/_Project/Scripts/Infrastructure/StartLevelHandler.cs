using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Scripts.Configs.Game;
using Scripts.Interfaces.Audio;
using Scripts.Interfaces.SDK;
using Scripts.Players.Logic;
using Scripts.Spawner;
using Scripts.Systems.Cameras;
using Scripts.Systems.Quests;
using Scripts.UI;
using UnityEngine;
using YG;
using Zenject;

namespace Scripts.Infrastructure
{
    public class StartLevelHandler : MonoBehaviour
    {
        [SerializeField] private float _unfadeCanvasDuration = 0.5f;
        [SerializeField] private GameObject _pointerCanvas;

        private List<BaseEnemySpawner> _enemySpawners;
        private QuestEnemyHandler _questEnemyHandler;
        private Player _player;
        private PlayerSpawner _playerSpawner;
        private CameraSystem _cameraSystem;
        private UiCanvas _uiCanvas;
        private IGameReadyService _gameReadyService;
        private JoystickCanvas _joystickCanvas;
        private GameConfig _gameConfig;
        private IAudioService _audioService;

        [Inject]
        private void Construct(
            List<BaseEnemySpawner> enemySpawners,
            QuestEnemyHandler questEnemyHandler,
            Player player,
            PlayerSpawner playerSpawner,
            CameraSystem cameraSystem,
            UiCanvas uiCanvas,
            IGameReadyService gameReadyService,
            IAudioService audioService,
            GameConfig gameConfig,
            JoystickCanvas joystickCanvas = null)
        {
            _enemySpawners = enemySpawners;
            _questEnemyHandler = questEnemyHandler;
            _player = player;
            _playerSpawner = playerSpawner;
            _cameraSystem = cameraSystem;
            _uiCanvas = uiCanvas;
            _gameReadyService = gameReadyService;
            _gameConfig = gameConfig;
            _audioService = audioService;
            _joystickCanvas = joystickCanvas;
        }

        private async UniTaskVoid Start()
        {
            _audioService.StopAudio();
            _audioService.PlayAmbience(_gameConfig.Ambience);
            _playerSpawner.Initialize();
            _player.DisableMove();
            DisableUi();

            await _cameraSystem.ShowOpeningAsync(destroyCancellationToken);

            _player.EnableMove();
            EnableUi();
            _gameReadyService.Call();
            YandexGame.GameplayStart();

            await _uiCanvas.EnableAsync(_unfadeCanvasDuration, destroyCancellationToken);

            PrepareSpawners();
            InitializeQuestEnemies();

            await UniTask.WaitUntil(() => Input.anyKey);

            _audioService.PlayMusic(_gameConfig.MainMusic);
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

        private void EnableUi()
        {
            if (_joystickCanvas != null)
                _joystickCanvas.Enable();

            if (_pointerCanvas != null)
                _pointerCanvas.SetActive(true);
        }

        private void DisableUi()
        {
            _uiCanvas.Disable();

            if (_joystickCanvas != null)
                _joystickCanvas.Disabe();

            if (_pointerCanvas != null)
                _pointerCanvas.SetActive(false);
        }
    }
}