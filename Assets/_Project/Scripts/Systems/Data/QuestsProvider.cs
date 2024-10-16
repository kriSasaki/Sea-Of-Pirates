using System.Collections.Generic;
using Scripts.Interfaces.Data;
using Scripts.Systems.Quests;

namespace Scripts.Systems.Data
{
    public class QuestsProvider : IQuestsProvider
    {
        private readonly IQuestsData _questsData;

        public QuestsProvider(IQuestsData questsData)
        {
            _questsData = questsData;
        }

        public Dictionary<string, QuestStatus> LoadQuests()
        {
            return _questsData.GetQuests();
        }

        public void UpdateQuest(string questID, QuestStatus status)
        {
            _questsData.UpdateQuestData(questID, status);
        }
    }
}