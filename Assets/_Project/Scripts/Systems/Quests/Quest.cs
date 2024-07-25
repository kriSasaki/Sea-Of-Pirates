using System;
using Project.Configs.Quests;
using Project.Enemies;
using Project.Interfaces.Enemies;
using Project.Interfaces.Quests;

namespace Project.Systems.Quests
{
    public class Quest : IQuest
    {
        private IEnemyDeathNotifier _enemyDeathNotifier;
        private QuestConfig _config;
        private QuestStatus _status;

        public Quest(QuestConfig config, QuestStatus status, IEnemyDeathNotifier enemyDeathNotifier)
        {
            _config = config;
            _status = status;
            _enemyDeathNotifier = enemyDeathNotifier;

            InitializeState();
        }

        public event Action<QuestStatus> StatusChanged;

        public QuestConfig Config => _config;
        public QuestStatus Status => _status;
        private int Progress => _status.Progress;
        private QuestState State => _status.State;

        public void UpdateState()
        {
            switch (State)
            {
                case QuestState.Avaliable:
                    Subscribe();
                    ChangeState(QuestState.Taken);
                    break;

                case QuestState.Taken:
                    CheckProgress();
                    break;

                case QuestState.Done:
                    ChangeState(QuestState.Completed);
                    break;

                case QuestState.Completed:
                    break;
            }
        }

        public void Unsubscribe()
        {
            _enemyDeathNotifier.EnemyDied -= OnEnemyDied;
        }

        private void Subscribe()
        {
            _enemyDeathNotifier.EnemyDied += OnEnemyDied;
        }

        private void InitializeState()
        {
            if (State == QuestState.Taken)
                Subscribe();
        }

        private void ChangeState(QuestState state)
        {
            _status.State = state;
            StatusChanged?.Invoke(_status);
        }

        private void CheckProgress()
        {
            if (IsDone())
            {
                Unsubscribe();
                ChangeState(QuestState.Done);
            }
        }

        private bool IsDone()
        {
            return Progress == _config.TargetAmount;
        }

        private void OnEnemyDied(EnemyConfig enemyType)
        {
            if (enemyType == _config.TargetType)
            {
                _status.Progress++;
                StatusChanged?.Invoke(_status);

                CheckProgress();
            }
        }
    }
}