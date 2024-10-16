using System;
using Scripts.Configs.GameResources;

namespace Scripts.Interfaces.Storage
{
    public interface IStorageNotifier
    {
        event Action<GameResource, int> ResourceAmountChanged;

        int GetResourceAmount(GameResource gameResource);
    }
}
