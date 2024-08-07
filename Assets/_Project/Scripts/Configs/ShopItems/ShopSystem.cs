using Agava.YandexGames;
using Project.Interfaces.Data;
using Project.UI.Upgrades;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopSystem : UiWindow
{
    [SerializeField] private ShopItemSlot _itemSlotPrefab;
    [SerializeField] private InAppItemSlot _inAppSlotPrefab;
    [SerializeField] private RectTransform _itemSlotsHolder;
    [SerializeField] private ConfirmWindow _confirmWindow;

    private InAppItemFactory _inAppItemfactory;
    private InAppItemsSheet _inAppitemsSheet;

    private readonly List<ShopItemSlot> _itemSlots = new();
    private readonly List<InAppItemSlot> _inAppItemSlots = new();

    private IPlayerStorage _playerStorage;
    private ShopItemsSheet _shopItemsSheet;

    public void Open()
    {
        base.Show();

        if (_itemSlots.Count > 0)
            return;

        LoadShopItems();
    }

    private void LoadShopItems()
    {
        foreach (GameResourceItem item in _shopItemsSheet.ShopItems)
        {
            ShopItemSlot itemSlot = Instantiate(_itemSlotPrefab, _itemSlotsHolder);
            itemSlot.Initialize(item);
            itemSlot.Selected += OnShopItemSelected;

            _itemSlots.Add(itemSlot);
        }
    }

    private void LoadInAppItems()
    {
        Agava.YandexGames.Billing.GetProductCatalog(productCatalogRespose => UpdateItemCatalog(productCatalogRespose.products));
    }


    private void UpdateItemCatalog(CatalogProduct[] products)
    {
        foreach (var product in products)
        {
            //if (_inAppitemsSheet.TryGetItemConfig(product.id, out InAppItemConfig itemConfig))
            //{
            //    InAppItem item = _inAppItemfactory.Create(itemConfig);

            //    if (item.IsPurchasable)
            //    {
            //        InAppItemSlot slot = UnityEngine.Object.Instantiate(_inAppSlotPrefab);

            //        InAppItemData itemData = new InAppItemData(
            //            itemConfig.Sprite,
            //            itemConfig.Amount,
            //            product.priceValue,
            //            product.priceCurrencyCode);

            //        slot.Initialize(item, itemData);
            //        slot.Selected += OnInAppItemSelected;

            //        _inAppItemSlots.Add(slot);
            //    }
            //}
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

    private void OnShopItemSelected(GameResourceItem shopItem)
    {
        if (_playerStorage.CanSpend(shopItem.Price))
        {
            _confirmWindow.Open(shopItem, () => BuyItem(shopItem));
        }
    }

    private void OnInAppItemSelected(InAppItem item, InAppItemData itemData)
    {
        _confirmWindow.Open(itemData, () => BuyItem(item));
        Agava.YandexGames.Billing.PurchaseProduct("@3", (d) =>
        {
            Agava.YandexGames.Billing.ConsumeProduct(d.purchaseData.purchaseToken, () =>
            {
                BuyItem(item);
            });
        });
    }


    private void BuyItem(InAppItem item)
    {
        item.Purcahse();
    }

    private void BuyItem(GameResourceItem shopItem)
    {
        _playerStorage.TrySpendResource(shopItem.Price);
        _playerStorage.AddResource(shopItem.Item);
    }
}
