using System;
using System.Collections.Generic;
using Project.Interfaces.Data;
using Zenject;

namespace Project.Systems.Quests
{
    public class QuestSystem : IDisposable
    {
        private IQuestsProvider _questsProvider;
        private List<QuestGiver> _questGivers;

        [Inject]
        public void Construct(List<QuestGiver> questGivers, IQuestsProvider questsProvider)
        {
            _questsProvider = questsProvider;
            _questGivers = questGivers;

            InitializeQuestGivers();
        }

        public void Dispose()
        {
            foreach (QuestGiver questGiver in _questGivers)
                questGiver.QuestStatusChanged -= OnQuestStatusChanged;
        }

        private void InitializeQuestGivers()
        {
            Dictionary<int, QuestStatus> quests = _questsProvider.LoadQuests();
            foreach (QuestGiver questGiver in _questGivers)
            {
                var id = questGiver.QuestID;
                QuestStatus questStatus = quests.ContainsKey(id) ? quests[id] : new QuestStatus();

                questGiver.Initialize(questStatus);

                questGiver.QuestStatusChanged += OnQuestStatusChanged;
            }
        }

        private void OnQuestStatusChanged(int id, QuestStatus status)
        {
            _questsProvider.UpdateQuest(id, status);
        }
    }
}