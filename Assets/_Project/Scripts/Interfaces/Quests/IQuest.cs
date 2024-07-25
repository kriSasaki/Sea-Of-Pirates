using Project.Configs.Quests;
using Project.Systems.Quests;

namespace Project.Interfaces.Quests
{
    public interface IQuest
    {
        QuestConfig Config { get; }
        QuestStatus Status { get; }

        void UpdateState();
    }
}