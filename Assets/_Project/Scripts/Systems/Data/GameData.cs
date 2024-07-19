using System.Collections.Generic;

namespace Project.Systems.Data
{
    [System.Serializable]
    public class GameData
    {
        public List<GameResourceData> Storage = new List<GameResourceData>();
    }
}