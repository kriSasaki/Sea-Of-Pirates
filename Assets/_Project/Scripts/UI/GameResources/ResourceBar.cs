using Scripts.Configs.GameResources;
using Scripts.Interfaces.Storage;
using Scripts.Utils.Extensions;
using Scripts.Utils.Tweens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] private GameResource _resource;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amount;
    [SerializeField] private ScaleTween _scaleTween;

    private IStorageNotifier _storage;

    private void OnDestroy()
    {
        _storage.ResourceAmountChanged -= OnResourceAmountChanged;
    }

    [Inject]
    public void Construct(IStorageNotifier storage)
    {
        _storage = storage;
        _icon.sprite = _resource.Sprite;

        int amount = _storage.GetResourceAmount(_resource);
        ChangeResourceAmount(amount);

        _storage.ResourceAmountChanged += OnResourceAmountChanged;
        _scaleTween.Initialize(_amount.transform);
    }

    private void OnResourceAmountChanged(GameResource resource, int amount)
    {
        if (resource != _resource)
            return;

        ChangeResourceAmount(amount);
        _scaleTween.RunFrom();
    }

    private void ChangeResourceAmount(int amount)
    {
        _amount.text = amount.ToNumericalString();
    }
}