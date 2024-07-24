using Project.Configs.GameResources;
using Project.Configs.Quests;
using Project.Enemies;
using Project.Interfaces.Data;
using Project.Interfaces.Enemies;
using Project.Systems.Stats;
using System;
using System.Linq;
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

public class QuestsProvider: IQuestsProvider
{
    private IQuestsData _questsData;
    private Dictionary<int, QuestStatus> _quests;

    public QuestsProvider(IQuestsData questsData)
    {
        _questsData = questsData;
        _quests = null;
    }

    public Dictionary<int, QuestStatus> LoadQuests()
    {
        if (_quests != null)
        {
            return _quests;
        }

        _quests = new Dictionary<int, QuestStatus>();

        foreach (QuestData questData in _questsData.Quests)
        {
            _quests.Add(questData.ID, new QuestStatus(questData.State, questData.Progress));
        }

        return _quests;
    }

    public void UpdateQuest(int questID, QuestStatus status)
    {
        _quests[questID] = status;

        if (_questsData.Quests.Any(q => q.ID == questID) == false)
        {
            _questsData.Quests.Add(new QuestData(questID, status));
        }

        QuestData data = _questsData.Quests.Find(q => q.ID == questID);
        data.Update(status);

        _questsData.Save();
    }
}

public interface IQuestsProvider
{
    Dictionary<int, QuestStatus> LoadQuests();
    void UpdateQuest(int id, QuestStatus status);
}

public class Quest
{
    private IEnemyDeathNotifier _enemyDeathNotifier;
    private QuestConfig _config;
    private QuestStatus _status;

    public Quest(QuestConfig config, QuestStatus status, IEnemyDeathNotifier enemyDeathNotifier)
    {
        _config = config;
        _status = status;

        _enemyDeathNotifier = enemyDeathNotifier;

        _enemyDeathNotifier.EnemyDied += OnEnemyDied;
    }

    public event Action<QuestStatus> StatusChanged;

    public int Progress => _status.Progress;
    public QuestState State => _status.State;

    private void OnEnemyDied(EnemyConfig enemyType)
    {
        if (enemyType == _config.TargetType)
        {
            _status.Progress++;
            StatusChanged?.Invoke(_status);
            CheckState();
        }
    }

    private void ChangeState(QuestState state)
    {
        _status.State = state;
        StatusChanged?.Invoke(_status);
    }

    private void CheckState()
    {
        switch (State)
        {
            case QuestState.Avaliable:
                ChangeState(QuestState.Taken);
                break;

            case QuestState.Taken:
                if (IsDone())
                {
                    ChangeState(QuestState.Done);
                }

                break;

            case QuestState.Done:
                ChangeState(QuestState.Completed);
                break;

            case QuestState.Completed:
                break;
        }
    }

    private bool IsDone()
    {
        return Progress == _config.TargetAmount;
    }
}

[System.Serializable]
public class QuestData
{
    public int ID;
    public QuestState State = QuestState.Avaliable;
    public int Progress;

    public QuestData(int id, QuestStatus status)
    {
        ID = id;
        State = status.State;
        Progress = status.Progress;
    }

    public void Update(QuestStatus status)
    {
        State = status.State;
        Progress = status.Progress;
    }
}



public class QuestGiver : MonoBehaviour
{
    [SerializeField] private QuestConfig _questConfig;

    private IEnemyDeathNotifier _enemyDeathNotifier;

    private Quest _quest;

    public event Action<int, QuestStatus> QuestStatusChanged;

    public int QuestID => _questConfig.ID;

    private void OnDestroy()
    {
        _quest.StatusChanged -= OnQuestStatusChanged;
    }

    public void Construct(IEnemyDeathNotifier enemyDeathNotifier)
    {
        _enemyDeathNotifier = enemyDeathNotifier;
    }

    public void Initialize(QuestStatus questStatus)
    {
        _quest = new Quest(_questConfig, questStatus, _enemyDeathNotifier);

        _quest.StatusChanged += OnQuestStatusChanged;
    }

    private void OnQuestStatusChanged(QuestStatus status)
    {
        QuestStatusChanged?.Invoke(QuestID, status);
    }
}

public class QuestSystem : MonoBehaviour
{
    private IQuestsProvider _questsProvider;
    private List<QuestGiver> _questGivers;

    private void OnDestroy()
    {
        foreach (QuestGiver questGiver in _questGivers)
            questGiver.QuestStatusChanged -= OnQuestStatusChanged;
    }

    public void Construct(List<QuestGiver> questGivers, IQuestsProvider questsProvider)
    {
        _questsProvider = questsProvider;
        _questGivers = questGivers;

        InitializeQuestGivers();
    }

    private void InitializeQuestGivers()
    {
        Dictionary<int, QuestStatus> quests = _questsProvider.LoadQuests();
        foreach (QuestGiver questGiver in _questGivers)
        {
            var id = questGiver.QuestID;
            QuestStatus questStatus = quests.ContainsKey(id) ? quests[id] : new QuestStatus();

            questGiver.Initialize(questStatus);

            questGiver.QuestStatusChanged += OnQuestStatusChanged;
        }
    }

    private void OnQuestStatusChanged(int id, QuestStatus status)
    {
        _questsProvider.UpdateQuest(id,status);
    }
}

public enum QuestState
{
    Avaliable,
    Taken,
    Done,
    Completed
}

public struct QuestStatus
{
    public QuestState State;
    public int Progress;

    public QuestStatus(QuestState state, int progress)
    {
        State = state;
        Progress = progress;
    }
}

public class QuestView : MonoBehaviour
{

}
