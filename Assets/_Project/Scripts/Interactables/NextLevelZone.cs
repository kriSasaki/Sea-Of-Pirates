using Lean.Localization;
using Scripts.Configs.Game;
using Scripts.Configs.GameResources;
using Scripts.Configs.Level;
using Scripts.Interfaces.Data;
using Scripts.Interfaces.SDK;
using Scripts.Players.Logic;
using Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Zenject;

namespace Scripts.Interactables
{
    public class NextLevelZone : InteractableZone
    {
        private const int MapAmount = 1;

        [SerializeField] private GameResource _map;
        [SerializeField, LeanTranslationName] private string _avaliablePassToken;
        [SerializeField, LeanTranslationName] private string _unavaliablePassToken;

        private IPlayerStorage _storage;
        private ILevelSceneService _levelSceneService;
        private IMetricaService _metricaService;
        private NextLevelWindow _window;
        private GameConfig _gameConfig;
        private LevelConfig _levelConfig;

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            bool hasMap = _storage.CanSpend(_map, MapAmount);

            string windowText = hasMap ?
                GetTranslatedText(_avaliablePassToken) :
                GetTranslatedText(_unavaliablePassToken);

            if (hasMap)
                _window.Open(windowText, hasMap, SwitchLevel);
            else
                _window.Open(windowText, hasMap, () => _window.Hide());
        }

        protected override void OnPlayerExited(Player player)
        {
            base.OnPlayerExited(player);

            _window.Hide();
        }

        [Inject]
        private void Construct(
            IPlayerStorage storage,
            ILevelSceneService levelSceneService,
            NextLevelWindow window,
            GameConfig gameConfig,
            LevelConfig levelConfig,
            IMetricaService metricaService)
        {
            _storage = storage;
            _levelSceneService = levelSceneService;
            _window = window;
            _gameConfig = gameConfig;
            _levelConfig = levelConfig;
            _metricaService = metricaService;
        }

        private void SwitchLevel()
        {
            if (_levelConfig.IsLastLevel)
            {
                _window.ShowEndGamePanel();
                return;
            }

            _storage.TrySpendResource(_map, MapAmount);
            _levelSceneService.UpdateCurrentLevel(_levelConfig.NextLevel);

            YandexGame.GameplayStop();
            _metricaService.SendLevelFinishedEvent(_levelConfig.name);
            DG.Tweening.DOTween.KillAll();

            SceneManager.LoadScene(_gameConfig.LoadingScene);
        }

        private string GetTranslatedText(string token)
        {
            return LeanLocalization.GetTranslationText(token);
        }
    }
}
