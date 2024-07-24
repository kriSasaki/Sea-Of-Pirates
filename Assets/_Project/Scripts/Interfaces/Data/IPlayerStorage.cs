using Project.Configs.GameResources;
using Project.Systems.Stats;

namespace Project.Interfaces.Data
{
    public interface IPlayerStorage
    {
        void AddResource(GameResource gameResource, int amount);
        void AddResource(GameResourceAmount gameResourceAmount);
        void TrySpendResource();
    }
}