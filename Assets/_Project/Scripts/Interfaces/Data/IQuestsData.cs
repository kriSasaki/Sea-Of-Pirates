using System.Collections.Generic;
using Scripts.Systems.Data;
using Scripts.Systems.Quests;

namespace Scripts.Interfaces.Data
{
    public interface IQuestsData : ISaveable
    {
        Dictionary<string, QuestStatus> GetQuests();

        void UpdateQuestData(string id, QuestStatus status);
    }
}