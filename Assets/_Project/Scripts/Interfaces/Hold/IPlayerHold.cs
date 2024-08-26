using Project.Systems.Data;
using System;

namespace Project.Interfaces.Hold
{
    public interface IPlayerHold
    {
        event Action<int> CargoChanged;

        public  bool IsEmpty { get; }
        public int CargoSize { get; }

        void AddResource(GameResourceAmount gameResourceAmount);

        void LoadToStorage();
    }
}