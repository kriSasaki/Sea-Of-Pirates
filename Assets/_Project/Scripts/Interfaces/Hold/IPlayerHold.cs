using System;
using Scripts.Systems.Data;

namespace Scripts.Interfaces.Hold
{
    public interface IPlayerHold
    {
        event Action<int> CargoChanged;
        event Action Filled;

        public bool IsEmpty { get; }
        public int CargoSize { get; }

        void AddResource(GameResourceAmount gameResourceAmount);
        void LoadToStorage();
    }
}