using Project.Configs.GameResources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IResourceStorageProvider
{
    Dictionary<GameResource, int> LoadStorage();

    void UpdateStorage(Dictionary<GameResource, int> storage);
}

public class ResourceStorageProvider : IResourceStorageProvider
{
    private readonly IResourceStorageData _storageData;
    private readonly GameResourcesSheet _resourcesSheet;
    public ResourceStorageProvider(IResourceStorageData storageData, GameResourcesSheet resourcesSheet)
    {
        _storageData = storageData;
        _resourcesSheet = resourcesSheet;
    }

    public Dictionary<GameResource, int> LoadStorage()
    {
        Dictionary<GameResource, int> storage = new Dictionary<GameResource, int>();

        foreach (GameResource resource in _resourcesSheet.GameResources)
        {
            GameResourceData data = _storageData.Resources.FirstOrDefault(r => r.ID == resource.ID);

            if (data != null)
            {
                storage.Add(resource, data.Value);
            }
            else
            {
                storage.Add(resource, 0);
            }
        }

        return storage;
    }

    public void UpdateStorage(Dictionary<GameResource, int> storage)
    {
        foreach (GameResource resource in _resourcesSheet.GameResources)
        {
            GameResourceData data = _storageData.Resources.FirstOrDefault(r => r.ID == resource.ID);

            if (data != null)
            {
                data.Value = storage[resource];
            }
            else
            {
                _storageData.Resources.Add(new GameResourceData() { ID = resource.ID, Value = storage[resource] });
            }
        }
    }
}

[System.Serializable]
public class GameData
{
    public List<GameResourceData> Storage = new List<GameResourceData>();
}

public class GameDataService : IResourceStorageData
{
    private const string SaveKey = nameof(SaveKey);

    private readonly GameData _gameData;

    public GameDataService()
    {
        GameData data = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(SaveKey, null));

        _gameData = data ?? new GameData();
    }

    public List<GameResourceData> Resources => _gameData.Storage;

    public void Save()
    {
        string json = JsonUtility.ToJson(_gameData);

        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }
}

public interface IResourceStorageData : ISaveable
{
    List<GameResourceData> Resources { get; }
}

public interface ISaveable
{
    void Save();
}

[System.Serializable]
public class GameResourceData
{
    public int ID;
    public int Value;
}
