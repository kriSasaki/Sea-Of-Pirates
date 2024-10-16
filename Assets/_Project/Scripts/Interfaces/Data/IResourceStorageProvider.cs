using System.Collections.Generic;
using Scripts.Configs.GameResources;

namespace Scripts.Interfaces.Data
{
    public interface IResourceStorageProvider
    {
        Dictionary<GameResource, int> LoadStorage();

        void UpdateStorage();
    }
}