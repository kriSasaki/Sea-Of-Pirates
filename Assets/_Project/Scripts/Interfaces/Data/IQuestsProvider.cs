using Project.Systems.Quests;
using System.Collections.Generic;

namespace Project.Interfaces.Data
{
    public interface IQuestsProvider
    {
        Dictionary<int, QuestStatus> LoadQuests();
        void UpdateQuest(int id, QuestStatus status);
    }
}