using Project.Configs.GameResources;
using Project.Configs.Quests;
using Project.Enemies;
using Project.Interfaces.Enemies;
using Project.Systems.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Interfaces.Data;
using Project.Systems.Quests;
using UnityEngine.UI;
using TMPro;

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

    public QuestStatus Status => _status;
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

public class QuestView : MonoBehaviour
{
    [SerializeField] Canvas _questWindow;
    [SerializeField] private Button _button;

    [SerializeField] private TMP_Text _decription;

    private void Awake()
    {
        HideQuestWindow();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HUI);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HUI);
    }

    private void HUI()
    {
        throw new NotImplementedException();
    }

    public void ShowQuestInfo(QuestConfig questConfig, QuestStatus questStatus)
    {
        ShowQuestWindow();
    }

    public void HideQuestInfo()
    {
        HideQuestWindow();
    }

    private void ShowQuestWindow()
    {
        _questWindow.enabled = true;
    }

    private void HideQuestWindow()
    {
        _questWindow.enabled = false;
    }

}
