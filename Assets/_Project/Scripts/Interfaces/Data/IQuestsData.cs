using Project.Systems.Data;
using System.Collections.Generic;

namespace Project.Interfaces.Data
{
    public interface IQuestsData : ISaveable
    {
        List<QuestData> Quests { get; }
    }
}