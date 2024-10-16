using System.Collections.Generic;
using Scripts.Interfaces.SDK;
using YG;

namespace Scripts.SDK
{
    public class MetricaService : IMetricaService
    {
        private const string LevelMetricaID = "level";
        private const string DeadMetricaID = "dead";
        private const string QuestMetricaID = "quest";

        public void SendLevelFinishedEvent(string levelName)
        {
            SendMetrica(LevelMetricaID, levelName);
        }

        public void SendQuestDoneEvent(string questID)
        {
            SendMetrica(QuestMetricaID, questID);
        }

        public void SendPlayerDieEvent()
        {
            SendMetrica(DeadMetricaID);
        }

        private void SendMetrica(string target, string name)
        {
            var eventParams = new Dictionary<string, string>
            {
                { target, name },
            };

            YandexMetrica.Send(target, eventParams);
        }

        private void SendMetrica(string target)
        {
            YandexMetrica.Send(target);
        }
    }
}