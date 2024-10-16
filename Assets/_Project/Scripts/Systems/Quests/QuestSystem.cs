using System;
using System.Collections.Generic;
using System.Linq;
using Ami.BroAudio;
using Scripts.Configs.Game;
using Scripts.Interfaces.Audio;
using Scripts.Interfaces.Data;
using Scripts.Interfaces.SDK;
using Zenject;

namespace Scripts.Systems.Quests
{
    public class QuestSystem : IDisposable, IInitializable
    {
        private readonly IQuestsProvider _questsProvider;
        private readonly IAudioService _audioService;
        private readonly IMetricaService _metricaService;
        private readonly List<QuestGiver> _questGivers;
        private readonly SoundID _questDoneSound;

        public QuestSystem(
            List<QuestGiver> questGivers,
            IQuestsProvider questsProvider,
            IAudioService audioService,
            IMetricaService metricaService,
            GameConfig config)
        {
            _questsProvider = questsProvider;
            _questGivers = questGivers;
            _audioService = audioService;
            _metricaService = metricaService;
            _questDoneSound = config.QuestDoneSound;
        }

        public void Initialize()
        {
            InitializeQuestGivers();
        }

        public void Dispose()
        {
            foreach (QuestGiver questGiver in _questGivers)
                questGiver.QuestStatusChanged -= OnQuestStatusChanged;
        }

        public IEnumerable<Quest> GetQuests()
        {
            return _questGivers.Select(q => q.Quest);
        }

        private void InitializeQuestGivers()
        {
            Dictionary<string, QuestStatus> quests = _questsProvider.LoadQuests();
            foreach (QuestGiver questGiver in _questGivers)
            {
                var id = questGiver.QuestID;
                QuestStatus questStatus = quests.ContainsKey(id) ? quests[id] : new QuestStatus();

                questGiver.Initialize(questStatus);

                questGiver.QuestStatusChanged += OnQuestStatusChanged;
            }
        }

        private void OnQuestStatusChanged(string id, QuestStatus status)
        {
            _questsProvider.UpdateQuest(id, status);

            if (status.State == QuestState.Done)
            {
                _audioService.PlaySound(_questDoneSound);
                _metricaService.SendQuestDoneEvent(id);
            }
        }
    }
}