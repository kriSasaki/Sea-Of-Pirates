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

        public void AddResource(GameResource gameResource, int amount)
        {
            _storage.Add(gameResource, amount);
        }

        public void AddResource(GameResourceAmount gameResourceAmount)
        {
            
        }

        public void TrySpendResource()
        {

        }

        private void SpendResource()
        {

        }
    }
}