using Project.Configs.GameResources;
using Project.Configs.Quests;
using Project.Enemies;
using Project.Interfaces.Data;
using Project.Systems.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Configs.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Configs/Quest")]
    public class QuestConfig : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public EnemyConfig TargetType { get; private set; }
        [field: SerializeField, Min(1)] public int TargetAmount { get; private set; }
        [field: SerializeField] public GameResourceAmount Reward { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
    }
}

public interface IQuestsData : ISaveable
{
    List<QuestData> Quests { get; }
}

public class QuestsProvider
{

}

public interface IQuestsProvider
{
    Dictionary<int, QuestStatus> LoadQuests();
    void UpdateQuestStatus(QuestStatus status);
}

public class Quest
{

}

[System.Serializable]
public class QuestData
{
    public int ID;
    public QuestState State = QuestState.Avaliable;
    public int Progress;
}



public class QuestGiver : MonoBehaviour
{
    [SerializeField] private QuestConfig _questConfig;
}

public class QuestSystem : MonoBehaviour
{

}

public enum QuestState
{
    Avaliable,
    Taken,
    Done,
    Completed
}

public class QuestStatus
{
    public QuestState State { get; private set; }
    public int Progress;
}

public class QuestView : MonoBehaviour
{

}
