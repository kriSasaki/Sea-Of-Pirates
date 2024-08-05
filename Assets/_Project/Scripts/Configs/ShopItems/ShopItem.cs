using Project.Interfaces.Data;
using Project.Systems.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Configs/ShopItem")]

public class ShopItem : ScriptableObject
{
    [field: SerializeField] public GameResourceAmount Good { get; private set; }
    [field: SerializeField] public GameResourceAmount Price { get; private set; }
}

public class ShopItemsSheet
{
    [SerializeField] private List<ShopItem> _shopItems;

    public IEnumerable<ShopItem> ShopItems => _shopItems;
}

public class ShopSystem: MonoBehaviour
{
    [SerializeField] private ShopItemPanel _shopItemPanel;

    private IPlayerStorage _playerStorage;
    private ShopItemsSheet _shopItemsSheet;
    private List<ShopItemPanel> _shopItemPanels = new(); 

    private void Awake()
    {
        foreach (var item in _shopItemsSheet.ShopItems)
        {
            ShopItemPanel itemPanel = Instantiate(_shopItemPanel);
            itemPanel.Initialize(item);
            itemPanel.ShopItemSelected += OnShopItemSelected;
            _shopItemPanels.Add(itemPanel);
        }
    }

    private void OnDestroy()
    {
        foreach (var itemPanel in _shopItemPanels)
        {
            itemPanel.ShopItemSelected -= OnShopItemSelected;
        }
    }

    private void OnShopItemSelected(ShopItem shopItem)
    {
        if (_playerStorage.CanSpend(shopItem.Price))
        {
            _playerStorage.TrySpendResource(shopItem.Price);
            _playerStorage.AddResource(shopItem.Good);
        }
    }
}

public class ShopItemPanel:MonoBehaviour
{
    [SerializeField] private Button _selectButton;

    private Image _goodImage;
    private TMP_Text _goodAmount;
    private Image _priceImage;
    private TMP_Text _priceAmount;

    private ShopItem _shopItem;

    public event Action <ShopItem> ShopItemSelected;

    private void Awake()
    {
        _selectButton.onClick.AddListener(OnItemSelected);
    }

    private void OnDestroy()
    {
        _selectButton.onClick.RemoveListener(OnItemSelected);
    }

    private void OnItemSelected()
    {
        ShopItemSelected?.Invoke(_shopItem); 
    }

    public void Initialize(ShopItem shopItem)
    {
        _shopItem = shopItem;

        GameResourceAmount good = _shopItem.Good;
        GameResourceAmount price = _shopItem.Price;

        _goodImage.sprite = good.Resource.Sprite;
        _goodAmount.text = good.Amount.ToString();
        _priceImage.sprite = price.Resource.Sprite;
        _priceAmount.text = price.Amount.ToString();
    }
}