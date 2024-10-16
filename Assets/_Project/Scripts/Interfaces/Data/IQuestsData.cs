using System.Collections.Generic;
using Scripts.Systems.Data;

namespace Scripts.Interfaces.Data
{
    public interface IQuestsData : ISaveable
    {
        List<QuestData> Quests { get; }
    }
}