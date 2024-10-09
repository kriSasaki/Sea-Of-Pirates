using System.Collections.Generic;
using Project.Systems.Data;

namespace Project.Interfaces.Data
{
    public interface IQuestsData : ISaveable
    {
        List<QuestData> Quests { get; }
    }
}