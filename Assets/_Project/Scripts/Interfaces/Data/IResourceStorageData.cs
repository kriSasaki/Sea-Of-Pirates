using System.Collections.Generic;
using Project.Systems.Data;

namespace Project.Interfaces.Data
{
    public interface IResourceStorageData : ISaveable
    {
        List<GameResourceData> Storage { get; }
    }
}