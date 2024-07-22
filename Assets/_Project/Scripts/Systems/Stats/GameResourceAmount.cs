﻿using Project.Configs.GameResources;

namespace Project.Systems.Stats
{
    public struct GameResourceAmount
    {
        public GameResource Resource;
        public int Amount;

        public GameResourceAmount(GameResource resource, int amount)
        {
            Resource = resource;
            Amount = amount;
        }
    }
}