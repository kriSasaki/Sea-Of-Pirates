using Project.Configs.GameResources;
using System.Collections.Generic;
using UnityEngine;

public class TestEntryPointforDataScene : MonoBehaviour
{
    [SerializeField] private GameResourcesSheet _resourcesSheet;
    [SerializeField] private GameResource _res;

    private Dictionary<GameResource, int> _storage;

    private ResourceStorageProvider _resourceStorageProvider;
    private GameDataService _saveLoadService;

    private void Awake()
    {
        _saveLoadService = new GameDataService();
        _resourceStorageProvider = new ResourceStorageProvider(_saveLoadService, _resourcesSheet);
    }

    private void Start()
    {
        _storage = _resourceStorageProvider.LoadStorage();
        ShowStorage();
    }

    private void ShowStorage()
    {
        foreach (KeyValuePair<GameResource, int> resource in _storage)
        {
            Debug.Log($"{resource.Key} - {resource.Value}");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _storage[_res] += 5;
            ShowStorage();
            _resourceStorageProvider.UpdateStorage(_storage);

            foreach(GameResourceData t in _saveLoadService.Resources)
            {
                Debug.Log($"{t.ID}  is {t.Value}");
            }
            _saveLoadService.Save();
        }
    }
}
