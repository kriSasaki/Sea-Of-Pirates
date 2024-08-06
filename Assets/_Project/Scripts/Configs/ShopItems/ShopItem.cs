using Agava.YandexGames;
using Project.Interfaces.Data;
using Project.SDK.Advertisment;
using Project.Systems.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Configs/Shop/ShopItem")]

public class ShopItem : ScriptableObject
{
    [field: SerializeField] public GameResourceAmount Item { get; private set; }
    [field: SerializeField] public GameResourceAmount Price { get; private set; }
}

public abstract class InAppItemConfig : ScriptableObject
{
    public string ID { get; private set; }

    public Sprite Sprite { get; private set; }

    public abstract string Amount { get; }
}

[CreateAssetMenu(fileName = "ResourceItem", menuName = "Configs/Shop/InApp/ResourceItem")]
public class ResourceItemConfig : InAppItemConfig
{
    [field: SerializeField] public GameResourceAmount Item { get; private set; }

    public override string Amount => Item.Amount.ToString();
}

[CreateAssetMenu(fileName = "AddRemoval", menuName = "Configs/Shop/InApp/AddRemoval")]
public class AddRemovalConfig : InAppItemConfig
{
    public override string Amount => string.Empty;
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


public abstract class InAppItem
{
    public virtual bool IsPurchasable { get; } = true;

    public abstract void Purcahse();
}


public class AddRemovalItem : InAppItem
{
    private readonly StickyController _stickyController;

    public AddRemovalItem(StickyController stickyController)
    {
        _stickyController = stickyController;
    }

    public override bool IsPurchasable => _stickyController.IsStickyActive;

    public override void Purcahse()
    {
        _stickyController.HideSticky();
    }
}

public class ResourceItem : InAppItem
{
    private readonly IPlayerStorage _playerStorage;

    public ResourceItem(IPlayerStorage playerStorage, GameResourceAmount item)
    {
        _playerStorage = playerStorage;
        Item = item;
    }

    public GameResourceAmount Item { get; private set; }

    public override void Purcahse()
    {
        _playerStorage.AddResource(Item);
    }
}

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
                InAppItem item = _inAppItemfactory.Create(itemConfig);

                if (item.IsPurchasable)
                {
                    InAppItemSlot slot = UnityEngine.Object.Instantiate(_inAppSlotPrefab);

                    InAppItemData itemData = new InAppItemData(
                        itemConfig.Sprite,
                        itemConfig.Amount,
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
    private StickyController _stickyController;

    public InAppItemFactory(IPlayerStorage playerStorage, StickyController stickyController)
    {
        _playerStorage = playerStorage;
        _stickyController = stickyController;
    }

    public InAppItem Create(InAppItemConfig itemConfig)
    {
        if (itemConfig is ResourceItemConfig resourceConfig)
            return new ResourceItem(_playerStorage, resourceConfig.Item);

        if (itemConfig is AddRemovalConfig addRemovalConfig)
            return new AddRemovalItem(_stickyController);

        throw new NotImplementedException();
    }
}


