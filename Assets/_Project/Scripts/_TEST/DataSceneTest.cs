using System.Collections.Generic;
using Project.Configs.GameResources;
using Project.Interfaces.Data;
using Project.Systems.Data;
using UnityEngine;
using Zenject;

public class DataSceneTest : MonoBehaviour
{
    [SerializeField] private GameResource _resource;

    private GameResourcesSheet _resourcesSheet;
    private IResourceStorageProvider _resourceStorageProvider;
    private IResourceStorageData _resourceStorageData;

    private Dictionary<GameResource, int> _storage;

    [Inject]
    public void Construct(
        GameResourcesSheet resourcesSheet,
        IResourceStorageProvider resourceStorageProvider, 
        IResourceStorageData resourceStorageData)
    {
        _resourcesSheet = resourcesSheet;
        _resourceStorageProvider = resourceStorageProvider;
        _resourceStorageData = resourceStorageData;

        _storage = _resourceStorageProvider.LoadStorage();
    }

    private void Start()
    {
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
            _storage[_resource] += 5;

            ShowStorage();

            foreach(GameResourceData resource in _resourceStorageData.Storage)
            {
                Debug.Log($"{resource.ID}  is {resource.Value}");
            }
       
            _resourceStorageProvider.UpdateStorage(_storage);
        }
    }
}
