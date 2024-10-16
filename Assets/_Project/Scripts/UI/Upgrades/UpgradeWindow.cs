using System.Collections.Generic;
using Ami.BroAudio;
using Scripts.Configs.UI;
using Scripts.Interfaces.Audio;
using Scripts.Interfaces.Data;
using Scripts.Interfaces.Stats;
using UnityEngine;
using Zenject;

namespace Scripts.UI.Upgrades
{
    [RequireComponent(typeof(Canvas))]
    public class UpgradeWindow : UiWindow
    {
        [SerializeField] private StatUpgradeBar _barPrefab;
        [SerializeField] private RectTransform _barHolder;

        private readonly List<StatUpgradeBar> _bars = new();

        private StatsSheet _statsSheet;
        private IUpgradableStats _stats;
        private IPlayerStorage _playerStorage;
        private IAudioService _audioService;
        private SoundID _upgradeSound;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (StatUpgradeBar bar in _bars)
                bar.StatUpgraded -= OnStatUpgraded;
        }

        public void Open()
        {
            Show();

            foreach (StatUpgradeBar bar in _bars)
            {
                bar.Fill();
            }
        }

        [Inject]
        private void Construct(
            StatsSheet statsSheet,
            IUpgradableStats stats,
            IPlayerStorage playerStorage,
            IAudioService audioService,
            UiConfigs config)
        {
            _statsSheet = statsSheet;
            _stats = stats;
            _playerStorage = playerStorage;
            _audioService = audioService;
            _upgradeSound = config.UpgradeSound;
            CreateUpgradeBars();
        }

        private void CreateUpgradeBars()
        {
            foreach (StatConfig statConfig in _statsSheet.Stats)
            {
                StatUpgradeBar upgradeBar = Instantiate(_barPrefab, _barHolder);
                upgradeBar.Initialize(statConfig, _stats, _playerStorage);
                upgradeBar.StatUpgraded += OnStatUpgraded;

                _bars.Add(upgradeBar);
            }
        }

        private void OnStatUpgraded()
        {
            _audioService.PlaySound(_upgradeSound);

            foreach (StatUpgradeBar bar in _bars)
            {
                bar.CheckUpgradePrice();
            }
        }
    }
}