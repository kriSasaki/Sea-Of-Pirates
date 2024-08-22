using System;
using System.Collections.Generic;
using System.Linq;
using Project.Interfaces.Data;
using Project.Interfaces.Hold;
using Project.Interfaces.Stats;
using Project.Systems.Stats;
using UnityEngine;
using Zenject;

namespace Project.Players.Hold
{
    public class PlayerHold : IPlayerHold, IInitializable
    {
        private readonly IPlayerStats _playerStats;
        private readonly IPlayerStorage _playerStorage;
        private readonly List<GameResourceAmount> _cargo;

        public PlayerHold(IPlayerStats playerStats, IPlayerStorage playerStorage)
        {
            _playerStats = playerStats;
            _playerStorage = playerStorage;
            _cargo = new List<GameResourceAmount>();
        }

        public event Action<int> CargoChanged;

        public int CargoSize => _playerStats.CargoSize;

        public bool IsEmpty => _cargo.Count == 0;

        public void Initialize()
        {
            CargoChanged?.Invoke(GetResourcesAmount());
        }

        public void AddResource(GameResourceAmount gameResourceAmount)
        {
            int inHoldCargo = GetResourcesAmount();

            if (inHoldCargo == CargoSize)
                return;

            gameResourceAmount.Amount = Mathf.Min(gameResourceAmount.Amount, CargoSize - inHoldCargo);
            _cargo.Add(gameResourceAmount);

            CargoChanged?.Invoke(GetResourcesAmount());
        }

        public void LoadToStorage()
        {
            _playerStorage.AddResource(_cargo);
            _cargo.Clear();

            CargoChanged?.Invoke(GetResourcesAmount());
        }

        private int GetResourcesAmount()
        {
            return _cargo.Sum(resource => resource.Amount);
        }
    }
}