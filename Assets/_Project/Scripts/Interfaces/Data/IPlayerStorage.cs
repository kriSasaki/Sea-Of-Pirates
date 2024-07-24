using System.Collections.Generic;
using Project.Configs.GameResources;
using Project.Systems.Stats;

namespace Project.Interfaces.Data
{
    public interface IPlayerStorage
    {
        void AddResource(GameResource gameResource, int amount);
        void AddResource(GameResourceAmount gameResourceAmount);
        void AddResource(List<GameResourceAmount> gameResourcesAmounts);
        void TrySpendResource(GameResource gameResource, int amount);
        void TrySpendResource(GameResourceAmount gameResourceAmount);
        void TrySpendResource(List<GameResourceAmount> gameResourcesAmounts);
        bool CanSpend(GameResource gameResource, int amount);
    }
}