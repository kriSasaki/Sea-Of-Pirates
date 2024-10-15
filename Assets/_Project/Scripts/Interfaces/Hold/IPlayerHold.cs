using System;
using Project.Systems.Data;

namespace Project.Interfaces.Hold
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