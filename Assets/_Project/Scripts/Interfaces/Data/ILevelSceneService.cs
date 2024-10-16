namespace Scripts.Interfaces.Data
{
    public interface ILevelSceneService
    {
        string CurrentLevel { get; }

        void UpdateCurrentLevel(string levelName);
    }
}