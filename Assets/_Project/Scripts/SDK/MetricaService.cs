using Project.Interfaces.SDK;
using System.Collections.Generic;
using YG;

namespace Project.SDK
{
    public class MetricaService : IMetricaService
    {
        private const string LevelMetricID = "level";
        private const string DeadMetricID = "dead";
        private const string QuestMetricID = "quest";

        public void SendLevelFinishedEvent(string levelName)
        {
            SendMetrica(LevelMetricID, levelName);
        }

        public void SendQuestDoneEvent(string questID)
        {
            SendMetrica(QuestMetricID, questID);
        }

        public void SendPlayerDieEvent()
        {
            SendMetrica(DeadMetricID);
        }

        private void SendMetrica(string target, string name)
        {
            var eventParams = new Dictionary<string, string>
            {
                {target, name }
            };

            YandexMetrica.Send(target, eventParams);
        }

        private void SendMetrica(string target)
        {
            YandexMetrica.Send(target);
        }
    }
}