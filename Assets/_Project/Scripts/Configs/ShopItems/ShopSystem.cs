using Project.Interfaces.Data;
using Project.UI.Upgrades;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopSystem : UiWindow
{
    [SerializeField] private ShopItemSlot _itemSlotPrefab;
    [SerializeField] private RectTransform _itemSlotsHolder;

    private readonly List<ShopItemSlot> _itemSlots = new();
    
    private IPlayerStorage _playerStorage;
    private ShopItemsSheet _shopItemsSheet;

    public override void Show()
    {
        base.Show();

        if (_itemSlots.Count > 0)
            return;

        foreach (var item in _shopItemsSheet.ShopItems)
        {
            ShopItemSlot itemSlot = Instantiate(_itemSlotPrefab,_itemSlotsHolder);
            itemSlot.Initialize(item);
            itemSlot.Selected += OnShopItemSelected;

            _itemSlots.Add(itemSlot);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
 
        foreach (var itemSlot in _itemSlots)
        {
            itemSlot.Selected -= OnShopItemSelected;
        }
    }

    [Inject]
    public void Construct(IPlayerStorage playerStorage, ShopItemsSheet shopItemsSheet)
    {
        _playerStorage = playerStorage;
        _shopItemsSheet = shopItemsSheet;
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