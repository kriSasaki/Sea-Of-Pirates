using Project.Systems.Shop.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI.Shop
{
    public class ShopWindow : UiWindow
    {
        [SerializeField] private ShopItemSlot _itemSlotPrefab;
        [SerializeField] private RectTransform _itemSlotsHolder;
        [SerializeField] private ConfirmWindow _confirmWindow;

        private readonly Dictionary<ShopItem, ShopItemSlot> _itemSlots = new();

        public void Open()
        {
            Show();
        }

        public void CreateItemSlot(GameItem item, Action onBuyCallback)
        {
            var itemSlot = Instantiate(_itemSlotPrefab, _itemSlotsHolder);
            itemSlot.Initialize(item, () => OnItemSelected(item, onBuyCallback));
            _itemSlots.Add(item, itemSlot);
        }

        public void CreateItemSlot(InAppItem item, Action onBuyCallback)
        {
            var itemSlot = Instantiate(_itemSlotPrefab, _itemSlotsHolder);
            itemSlot.Initialize(item, () => OnItemSelected(item, onBuyCallback));
            _itemSlots.Add(item, itemSlot);
        }

        public void RemoveItemSlot(ShopItem item)
        {
            if (!_itemSlots.ContainsKey(item))
                throw new ArgumentException($" {nameof(item)} отсутствует в магазине");

            Destroy(_itemSlots[item].gameObject);
            _itemSlots.Remove(item);
        }

        private void OnItemSelected(GameItem item, Action onBuyCallback)
        {
            if (item.CanBuy)
            {
                _confirmWindow.Open(item, () => onBuyCallback());
            }
        }

        private void OnItemSelected(InAppItem item, Action onBuyCallback)
        {
            onBuyCallback?.Invoke();
        }
    }
}