using Scripts.Configs.GameResources;

namespace Scripts.Systems.Data
{
    [System.Serializable]
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