using System;
using Scripts.Configs.Quests;
using Scripts.Interactables;
using Scripts.Interfaces.Data;
using Scripts.Interfaces.Enemies;
using Scripts.Players.Logic;
using Scripts.UI.Quests;
using UnityEngine;
using Zenject;

namespace Scripts.Systems.Quests
{
    public class QuestGiver : InteractableZone
    {
        [SerializeField] private QuestConfig _questConfig;
        [SerializeField] private QuestMarker _questMarker;

        private IEnemyDeathNotifier _enemyDeathNotifier;
        private IPlayerStorage _playerStorage;
        private QuestView _questView;
        private Quest _quest;

        public event Action<string, QuestStatus> QuestStatusChanged;

        public string QuestID => _questConfig.ID;
        public Quest Quest => _quest;

        private void OnDestroy()
        {
            _quest.StatusChanged -= OnQuestStatusChanged;
            _quest.Unsubscribe();
        }

        public void Initialize(QuestStatus status)
        {
            _quest = new Quest(_questConfig, status, _enemyDeathNotifier);
            _questMarker.SetMarkerVisual(status.State);

            _quest.StatusChanged += OnQuestStatusChanged;

            if (_quest.Status.State == QuestState.Completed)
                RemoveZoneView();
        }

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            if (_quest.Status.State != QuestState.Completed)
                _questView.Show(_quest);
        }

        protected override void OnPlayerExited(Player player)
        {
            base.OnPlayerExited(player);
            _questView.Hide();
        }

        [Inject]
        private void Construct(
            IEnemyDeathNotifier enemyDeathNotifier,
            IPlayerStorage playerStorage,
            QuestView questView)
        {
            _enemyDeathNotifier = enemyDeathNotifier;
            _playerStorage = playerStorage;
            _questView = questView;
        }

        private void OnQuestStatusChanged(Quest quest)
        {
            if (quest.Status.State == QuestState.Completed)
            {
                _playerStorage.AddResource(_questConfig.Reward);
                _questView.Hide();

                RemoveZoneView();
            }

            _questMarker.SetMarkerVisual(quest.Status.State);

            QuestStatusChanged?.Invoke(QuestID, quest.Status);
        }

        private void RemoveZoneView()
        {
            var zoneView = GetComponentInChildren<InteractableZoneVisual>();

            if (zoneView != null)
            {
                Destroy(zoneView.gameObject);
            }
        }
    }
}