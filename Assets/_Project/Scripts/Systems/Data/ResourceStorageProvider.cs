using System.Collections.Generic;
using System.Linq;
using Scripts.Configs.GameResources;
using Scripts.Interfaces.Data;

namespace Scripts.Systems.Data
{
    public class ResourceStorageProvider : IResourceStorageProvider
    {
        private const int InitialResourceAmount = 0;
        private readonly IResourceStorageData _storageData;
        private readonly GameResourcesSheet _resourcesSheet;
        private Dictionary<GameResource, int> _storage;

        public ResourceStorageProvider(IResourceStorageData storageData, GameResourcesSheet resourcesSheet)
        {
            _storageData = storageData;
            _resourcesSheet = resourcesSheet;
            _storage = null;
        }

        public Dictionary<GameResource, int> LoadStorage()
        {
            _storage = new Dictionary<GameResource, int>();

            foreach (GameResource resource in _resourcesSheet.GameResources)
            {
                GameResourceData data = _storageData.GetResourceData(resource.ID);

                if (data != null)
                {
                    _storage.Add(resource, data.Value);
                }
                else
                {
                    _storage.Add(resource, InitialResourceAmount);
                }
            }

            return _storage;
        }

        public void UpdateStorage()
        {
            foreach (GameResource resource in _storage.Keys)
            {
                _storageData.UpdateResourceData(resource.ID, _storage[resource]);
            }
        }
    }
}