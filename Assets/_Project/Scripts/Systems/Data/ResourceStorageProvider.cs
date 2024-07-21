using Project.Configs.GameResources;
using Project.Interfaces.Data;
using System.Collections.Generic;
using System.Linq;

namespace Project.Systems.Data
{
    public class ResourceStorageProvider : IResourceStorageProvider
    {
        private readonly IResourceStorageData _storageData;
        private readonly GameResourcesSheet _resourcesSheet;

        public ResourceStorageProvider(IResourceStorageData storageData, GameResourcesSheet resourcesSheet)
        {
            _storageData = storageData;
            _resourcesSheet = resourcesSheet;
        }

        public Dictionary<GameResource, int> LoadStorage()
        {
            Dictionary<GameResource, int> storage = new Dictionary<GameResource, int>();

            foreach (GameResource resource in _resourcesSheet.GameResources)
            {
                GameResourceData data = _storageData.Storage.FirstOrDefault(r => r.ID == resource.ID);

                if (data != null)
                {
                    storage.Add(resource, data.Value);
                }
                else
                {
                    storage.Add(resource, 0);
                }
            }

            return storage;
        }

        public void UpdateStorage(Dictionary<GameResource, int> storage)
        {
            foreach (GameResource resource in _resourcesSheet.GameResources)
            {
                GameResourceData data = _storageData.Storage.FirstOrDefault(r => r.ID == resource.ID);

                if (data != null)
                {
                    data.Value = storage[resource];
                }
                else
                {
                    _storageData.Storage.Add(new GameResourceData() { ID = resource.ID, Value = storage[resource] });
                }
            }

            _storageData.Save();
        }
    }
}