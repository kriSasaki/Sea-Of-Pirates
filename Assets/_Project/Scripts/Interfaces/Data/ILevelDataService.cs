using Project.Systems.Data;

namespace Project.Interfaces.Data
{
    public interface ILevelDataService
    {
        LevelData GetLevelData(string levelName);
    }
}