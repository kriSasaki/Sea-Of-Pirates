using Agava.YandexGames;
using Project.Enemies;
using Project.Interfaces.Data;
using Project.SDK.Advertisment;
using Project.Systems.Stats;
using Project.Systems.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


#region CONFIGS
[CreateAssetMenu(fileName = "ShopItem", menuName = "Configs/Shop/ShopItem")]

public class GameResourceItem : ScriptableObject
{
    [field: SerializeField] public GameResourceAmount Item { get; private set; }
    [field: SerializeField] public GameResourceAmount Price { get; private set; }
}

public abstract class InAppItemConfig : ScriptableObject
{
    public string ID { get; private set; }

    public Sprite Sprite { get; private set; }
}

[CreateAssetMenu(fileName = "InAppResourceItem", menuName = "Configs/Shop/InApp/InAppResource")]
public class InAppResourceItemConfig : InAppItemConfig
{
    [field: SerializeField] public GameResourceAmount Item { get; private set; }
}

[CreateAssetMenu(fileName = "AddRemoval", menuName = "Configs/Shop/InApp/AddRemoval")]
public class AddRemovalConfig : InAppItemConfig
{

}

[CreateAssetMenu(fileName = "InAppItems", menuName = "Configs/Shop/InApp/ItemsSheet")]
public class InAppItemsSheet : ScriptableObject
{
    [SerializeField] private List<InAppItemConfig> _items;

    public bool TryGetItemConfig(string id, out InAppItemConfig config)
    {
        config = _items.FirstOrDefault(x => x.ID == id);

        return config != null;
    }

    public IEnumerable<InAppItemConfig> Items => _items;
}

[CreateAssetMenu(fileName = "BundleItem", menuName = "Configs/Shop/InApp/BundleItem")]
public class BundleItemConfig : InAppItemConfig
{
    [SerializeField] private List<InAppItemConfig> _items;
    public IReadOnlyList<InAppItemConfig> Items => _items;
}
#endregion

#region Items
public abstract class InAppItem
{
    private readonly CatalogProduct _itemData;

    protected InAppItem(CatalogProduct itemData)
    {
        _itemData = itemData;
    }

    public abstract string AmountText { get; }
    public string ID => _itemData.id;
    public string PriceAmountText => $"{_itemData.priceValue} {_itemData.priceCurrencyCode}";

    public virtual bool IsPurchasable { get; } = true;

    public abstract void Purcahse();
}


public class AddRemovalItem : InAppItem
{
    private readonly AdvertismentController _advertismentController;

    public AddRemovalItem(AdvertismentController advertismentController, CatalogProduct itemData)
        : base(itemData)
    {
        _advertismentController = advertismentController;
    }

    public override bool IsPurchasable => _advertismentController.IsAddActive;

    public override string AmountText => string.Empty;

    public override void Purcahse()
    {
        _advertismentController.HideAdd();
    }
}

public class InAppResourceItem : InAppItem
{
    private readonly IPlayerStorage _playerStorage;
    private readonly InAppResourceItemConfig _config;

    public InAppResourceItem(
        IPlayerStorage playerStorage,
        InAppResourceItemConfig config,
        CatalogProduct itemData)
        : base(itemData)
    {
        _playerStorage = playerStorage;
        _config = config;
    }

    public GameResourceAmount Item => _config.Item;

    public override string AmountText => Item.Amount.ToString();

    public override void Purcahse()
    {
        _playerStorage.AddResource(Item);
    }
}

public class BundleItem : InAppItem
{
    private readonly List<InAppItem> _items;

    public BundleItem(List<InAppItem> items, CatalogProduct itemData) : base(itemData)
    {
        _items = items;
    }

    public override string AmountText => string.Empty;

    public override void Purcahse()
    {
        foreach (var item in _items)
        {
            item.Purcahse();
        }
    }
}
#endregion

public class InAppItemCatalog
{
    private InAppItemSlot _inAppSlotPrefab;
    private InAppItemFactory _inAppItemfactory;
    private InAppItemsSheet _inAppitemsSheet;
    public void Load()
    {
        Agava.YandexGames.Billing.GetProductCatalog(productCatalogRespose => UpdateItemCatalog(productCatalogRespose.products));
    }

    private void UpdateItemCatalog(CatalogProduct[] products)
    {
        foreach (var product in products)
        {
            if (_inAppitemsSheet.TryGetItemConfig(product.id, out InAppItemConfig itemConfig))
            {
                InAppItem item = _inAppItemfactory.Create(itemConfig, product);


                if (item.IsPurchasable)
                {
                    InAppItemSlot slot = UnityEngine.Object.Instantiate(_inAppSlotPrefab);

                    InAppItemData itemData = new InAppItemData(
                        itemConfig.Sprite,
                        "∆Œœ¿",
                        product.priceValue,
                        product.priceCurrencyCode);

                    slot.Initialize(item, itemData);
                }
            }
        }
    }
}

public class InAppItemFactory
{
    private IPlayerStorage _playerStorage;
    private AdvertismentController _stickyController;

    public InAppItemFactory(IPlayerStorage playerStorage, AdvertismentController advertismentController)
    {
        _playerStorage = playerStorage;
        _stickyController = advertismentController;
    }

    public InAppItem Create(InAppItemConfig itemConfig, CatalogProduct itemData)
    {
        if (itemConfig is InAppResourceItemConfig resourceConfig)
            return new InAppResourceItem(_playerStorage, resourceConfig, itemData);

        if (itemConfig is AddRemovalConfig addRemovalConfig)
            return new AddRemovalItem(_stickyController, itemData);

        if (itemConfig is BundleItemConfig bundleConfig)
            return new BundleItem(CreateBundle(bundleConfig, itemData), itemData);

        throw new NotImplementedException();
    }

    private List<InAppItem> CreateBundle(BundleItemConfig bundleConfig, CatalogProduct itemData)
    {
        List<InAppItem> _items = new List<InAppItem>();

        foreach (var item in bundleConfig.Items)
        {
            _items.Add(Create(item, itemData));
        }

        return _items;
    }
}


