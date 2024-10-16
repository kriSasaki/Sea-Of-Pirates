namespace Scripts.Systems.Quests
{
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
}