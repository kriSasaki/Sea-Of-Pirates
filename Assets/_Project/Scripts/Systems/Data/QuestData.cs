using Project.Systems.Quests;

namespace Project.Systems.Data
{
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
}