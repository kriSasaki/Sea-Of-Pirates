using Agava.YandexGames;
using Project.Configs.ShopItems;
using Project.Interfaces.Data;
using Project.Systems.Shop;
using Project.Systems.Shop.Items;
using Project.UI;
using Project.UI.Shop;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ShopSystem : UiWindow
{
    [SerializeField] private ShopItemSlot _itemSlotPrefab;
    [SerializeField] private RectTransform _itemSlotsHolder;
    [SerializeField] private ConfirmWindow _confirmWindow;

    private ShopItemFactory _shopItemfactory;

    private readonly List<ShopItemSlot> _itemSlots = new();

    private IPlayerStorage _playerStorage;
    private ShopItemsConfigs _shopItemsConfigs;

    public void Open()
    {
        base.Show();

        if (_itemSlots.Count > 0)
            return;

        LoadGameItems();
        LoadInAppItems();
    }

    private void LoadGameItems()
    {
        foreach (GameItemConfig config in _shopItemsConfigs.GameItemsConfigs)
        {
            GameItem item = _shopItemfactory.Create(config);

            ShopItemSlot itemSlot = CreateItemSlot();
            itemSlot.Initialize(item, () => OnItemSelected(item));
        }
    }

    private void LoadInAppItems(CatalogProduct[] products)
    {
        foreach (var config in _shopItemsConfigs.InAppItemsConfigs)
        {
            CatalogProduct itemData = products.FirstOrDefault(p => p.id == config.ID);

            if (itemData == null)
                continue;

            InAppItem item = _shopItemfactory.Create(config, itemData);

            if (item.IsAvaliable == false)
                continue;

            ShopItemSlot itemSlot = CreateItemSlot();
            itemSlot.Initialize(item, () => OnItemSelected(item));
        }
    }

    private ShopItemSlot CreateItemSlot()
    {
        return Instantiate(_itemSlotPrefab, _itemSlotsHolder);
    }

    private void LoadInAppItems()
    {
        Agava.YandexGames.Billing.GetProductCatalog(productCatalogRespose => LoadInAppItems(productCatalogRespose.products));
    }


    [Inject]
    public void Construct(IPlayerStorage playerStorage, ShopItemsConfigs shopItemsConfigs)
    {
        _playerStorage = playerStorage;
        _shopItemsConfigs = shopItemsConfigs;
    }

    private void OnItemSelected(GameItem item)
    {
        if (_playerStorage.CanSpend(item.Price))
        {
            _confirmWindow.Open(item, () => BuyItem(item));
        }
    }

    private void OnItemSelected(InAppItem item)
    {
        Agava.YandexGames.Billing.PurchaseProduct(item.ID, (purchaseProductResponse) =>
        {
            Agava.YandexGames.Billing.ConsumeProduct(purchaseProductResponse.purchaseData.purchaseToken, () =>
            {
                BuyItem(item);
            });
        });
    }

    private void BuyItem(InAppItem item)
    {
        GetShopItem(item);
    }    
    
    private void BuyItem(GameItem item)
    {
        if (_playerStorage.TrySpendResource(item.Price))
            GetShopItem(item);
    }
    
    private void GetShopItem(ShopItem item)
    {
        item.Get();
    }
}