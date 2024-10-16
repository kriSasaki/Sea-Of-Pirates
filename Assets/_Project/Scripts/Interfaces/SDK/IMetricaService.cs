namespace Scripts.Interfaces.SDK
{
    public interface IMetricaService
    {
        void SendLevelFinishedEvent(string levelName);

        void SendQuestDoneEvent(string questID);

        void SendPlayerDieEvent();
    }
}