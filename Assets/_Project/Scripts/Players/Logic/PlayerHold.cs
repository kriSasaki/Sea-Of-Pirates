using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Interfaces.Data;
using Scripts.Interfaces.Hold;
using Scripts.Interfaces.Stats;
using Scripts.Systems.Data;
using UnityEngine;
using Zenject;

namespace Scripts.Players.Logic
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
        public event Action Filled;

        public int CargoSize => _playerStats.CargoSize;
        public bool IsEmpty => _cargo.Count == 0;

        public void Initialize()
        {
            CargoChanged?.Invoke(GetResourcesAmount());
        }

        public void AddResource(GameResourceAmount gameResourceAmount)
        {
            if (gameResourceAmount.Resource == null)
                throw new ArgumentNullException(nameof(gameResourceAmount) + " GameResource is null!");

            int inHoldCargo = GetResourcesAmount();

            if (inHoldCargo == CargoSize)
            {
                Filled?.Invoke();
                return;
            }

            gameResourceAmount.Amount = Mathf.Min(gameResourceAmount.Amount, CargoSize - inHoldCargo);
            _cargo.Add(gameResourceAmount);

            CargoChanged?.Invoke(GetResourcesAmount());
        }

        public void LoadToStorage()
        {
            if (IsEmpty)
                return;

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