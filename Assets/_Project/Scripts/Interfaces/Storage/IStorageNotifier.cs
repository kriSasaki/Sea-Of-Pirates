using System;
using Project.Configs.GameResources;

namespace Project.Interfaces.Storage
{
    public interface IStorageNotifier
    {
        event Action<GameResource, int> ResourceAmountChanged;

        int GetResourceAmount(GameResource gameResource);
    }
}
