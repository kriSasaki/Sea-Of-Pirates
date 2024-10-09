using System.Collections.Generic;
using Project.Configs.GameResources;

namespace Project.Interfaces.Data
{
    public interface IResourceStorageProvider
    {
        Dictionary<GameResource, int> LoadStorage();
        void UpdateStorage();
    }
}