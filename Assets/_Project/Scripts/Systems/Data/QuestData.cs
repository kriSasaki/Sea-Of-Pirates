using Scripts.Systems.Quests;
using UnityEngine;

namespace Scripts.Systems.Data
{
    [System.Serializable]
    public class QuestData
    {
        [SerializeField] private string _id;
        [SerializeField] private QuestState _state;
        [SerializeField] private int _progress;

        public string ID => _id;
        public QuestState State => _state;
        public int Progress => _progress;

        public QuestData(string id, QuestStatus status)
        {
            _id = id;
            _state = status.State;
            _progress = status.Progress;
        }

        public void Update(QuestStatus status)
        {
            _state = status.State;
            _progress = status.Progress;
        }
    }
}