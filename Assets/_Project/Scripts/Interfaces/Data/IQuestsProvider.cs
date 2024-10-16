using System.Collections.Generic;
using Scripts.Systems.Quests;

namespace Scripts.Interfaces.Data
{
    public interface IQuestsProvider
    {
        Dictionary<string, QuestStatus> LoadQuests();

        void UpdateQuest(string id, QuestStatus status);
    }
}