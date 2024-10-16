using System.Collections.Generic;
using Scripts.Systems.Data;

namespace Scripts.Interfaces.Data
{
    public interface IResourceStorageData : ISaveable
    {
        GameResourceData GetResourceData(string id);

        void UpdateResourceData(string id, int value);
    }
}