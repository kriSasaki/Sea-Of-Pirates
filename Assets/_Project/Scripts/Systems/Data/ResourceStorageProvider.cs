using System.Collections.Generic;
using System.Linq;
using Project.Configs.GameResources;
using Project.Interfaces.Data;

namespace Project.Systems.Data
{
    public class ResourceStorageProvider : IResourceStorageProvider
    {
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
                GameResourceData data = _storageData.Storage.FirstOrDefault(r => r.ID == resource.ID);

                if (data != null)
                {
                    _storage.Add(resource, data.Value);
                }
                else
                {
                    _storage.Add(resource, 0);
                }
            }

            return _storage;
        }

        public void UpdateStorage()
        {
            foreach (GameResource resource in _resourcesSheet.GameResources)
            {
                GameResourceData data = _storageData.Storage.FirstOrDefault(r => r.ID == resource.ID);

                if (data != null)
                {
                    data.Value = _storage[resource];
                }
                else
                {
                    _storageData.Storage.Add(new GameResourceData() { ID = resource.ID, Value = _storage[resource] });
                }
            }

            _storageData.Save();
        }
    }
}