using Scripts.Configs.Quests;
using Scripts.Systems.Quests;

namespace Scripts.Interfaces.Quests
{
    public interface IQuest
    {
        QuestConfig Config { get; }

        QuestStatus Status { get; }

        void UpdateState();
    }
}