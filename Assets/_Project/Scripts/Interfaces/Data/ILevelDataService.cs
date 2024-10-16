using Scripts.Systems.Data;

namespace Scripts.Interfaces.Data
{
    public interface ILevelDataService
    {
        LevelData GetLevelData(string levelName);
    }
}