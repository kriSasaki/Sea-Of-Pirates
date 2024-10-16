using System.Collections.Generic;
using Scripts.Systems.Data;

namespace Scripts.Interfaces.Data
{
    public interface IResourceStorageData : ISaveable
    {
        List<GameResourceData> Storage { get; }
    }
}