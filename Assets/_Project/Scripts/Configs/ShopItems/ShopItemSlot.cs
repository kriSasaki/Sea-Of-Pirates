using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShopItemView))]
public class ShopItemSlot : ItemSlot
{
    private GameResourceItem _item;
    private ShopItemView _itemView;

    public event Action<GameResourceItem> Selected;

    public void Initialize(GameResourceItem item)
    {
        _item = item;

        _itemView = GetComponent<ShopItemView>();
        _itemView.Set(_item);
    }

    protected override void OnItemSelected()
    {
        Selected?.Invoke(_item);
    }
}

public class InAppItemSlot : ItemSlot
{
    private InAppItem _item;
    private ShopItemView _itemView;
    private InAppItemData _itemData;

    public event Action<InAppItem, InAppItemData> Selected;
    public void Initialize(InAppItem item, InAppItemData itemData)
    {
        _item = item;
        _itemData = itemData;

        _itemView = GetComponent<ShopItemView>();

        _itemView.Set(_itemData);
    }

    protected override void OnItemSelected()
    {
        Selected?.Invoke(_item, _itemData);
    }
}

public struct InAppItemData
{
    public Sprite ItemSprite;
    public string ItemAmount;
    public string PriceValue;
    public string PriceCurrencyCode;

    public InAppItemData(Sprite itemSprite, string itemAmount, string priceValue, string priceCurrencyCode)
    {
        ItemSprite = itemSprite;
        ItemAmount = itemAmount;
        PriceValue = priceValue;
        PriceCurrencyCode = priceCurrencyCode;
    }
}

public abstract class ItemSlot : MonoBehaviour
{
    [SerializeField] private Button _selectButton;

    private void Awake()
    {
        _selectButton.onClick.AddListener(OnItemSelected);
    }

    private void OnDestroy()
    {
        _selectButton.onClick.RemoveListener(OnItemSelected);
    }

    protected abstract void OnItemSelected();
}