using Project.Configs.GameResources;
using System.Collections.Generic;

namespace Project.Interfaces.Data
{
    public interface IResourceStorageProvider
    {
        Dictionary<GameResource, int> LoadStorage();

        void UpdateStorage();
    }
}