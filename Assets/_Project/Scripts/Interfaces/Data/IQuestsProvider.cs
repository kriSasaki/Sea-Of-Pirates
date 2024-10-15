using System.Collections.Generic;
using Project.Systems.Quests;

namespace Project.Interfaces.Data
{
    public interface IQuestsProvider
    {
        Dictionary<string, QuestStatus> LoadQuests();

        void UpdateQuest(string id, QuestStatus status);
    }
}