using System.Collections.Generic;
using Project.Configs.GameResources;
using Project.Interfaces.Data;
using Project.Systems.Stats;

namespace Project.Systems.Data
{
    public class PlayerStorage : IPlayerStorage
    {
        private readonly IResourceStorageProvider _resourceStorageProvider;
        private Dictionary<GameResource, int> _storage;

        public PlayerStorage(IResourceStorageProvider provider)
        {
            _resourceStorageProvider = provider;
            _storage = _resourceStorageProvider.LoadStorage();
        }

        public void AddResource(GameResource gameResource, int amount)
        {
            _storage[gameResource] += amount;
            SaveResources();
        }

        public void AddResource(GameResourceAmount gameResourceAmount)
        {
            AddResource(gameResourceAmount.Resource, gameResourceAmount.Amount);
        }

        public void AddResource(List<GameResourceAmount> gameResourcesAmounts)
        {
            foreach (var gameResourceAmount in gameResourcesAmounts)
            {
                AddResource(gameResourceAmount.Resource, gameResourceAmount.Amount);
            }
        }

        public void TrySpendResource(GameResource gameResource, int amount)
        {
            if (CanSpend(gameResource, amount))
            {
                SpendResource(gameResource, amount);
            }
        }

        public void TrySpendResource(GameResourceAmount gameResourceAmount)
        {
            TrySpendResource(gameResourceAmount.Resource, gameResourceAmount.Amount);
        }

        public void TrySpendResource(List<GameResourceAmount> gameResourcesAmounts)
        {
            foreach (var gameResourceAmount in gameResourcesAmounts)
            {
                TrySpendResource(gameResourceAmount.Resource, gameResourceAmount.Amount);
            }
        }

        public bool CanSpend(GameResource gameResource, int amount)
        {
            return _storage[gameResource] >= amount;
        }

        private void SpendResource(GameResource gameResource, int amount)
        {
            _storage[gameResource] -= amount;
            SaveResources();
        }

        private void SaveResources()
        {
            _resourceStorageProvider.UpdateStorage();
        }
    }
}