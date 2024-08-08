using Project.Configs.UI;
using Project.Systems.Shop.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI.Shop
{
    public class ShopWindow : UiWindow
    {
        [SerializeField] private ShopItemSlot _itemSlotPrefab;
        [SerializeField] private ConfirmWindow _confirmWindow;
        [SerializeField] private LayoutGroup _gameItemsLayout;
        [SerializeField] private LayoutGroup _inAppItemsLayout;

        private readonly List<ShopItemSlot> _itemSlots = new();

        private UiConfigs _uiConfigs;

        [Inject]
        public void Construct(UiConfigs uiConfigs)
        {
            _uiConfigs = uiConfigs;
        }

        public void Open()
        {
            Show();
        }

        public void CreateItemSlot(GameItem item, Action onBuyCallback)
        {
            ShopItemSlot itemSlot = Instantiate(_itemSlotPrefab, _gameItemsLayout.transform);

            itemSlot.Initialize(
                item,
                () => OnItemSelected(item, onBuyCallback),
                _uiConfigs.GameItemViewColor);

            _itemSlots.Add(itemSlot);
        }

        public void CreateItemSlot(InAppItem item, Action onBuyCallback)
        {
            ShopItemSlot itemSlot = Instantiate(_itemSlotPrefab, _inAppItemsLayout.transform);

            itemSlot.Initialize(
                item,
                () => OnItemSelected(item, onBuyCallback),
                _uiConfigs.InApptemViewColor);

            _itemSlots.Add(itemSlot);
        }

        public void CheckSlots()
        {
            foreach (var slot in _itemSlots)
            {
                slot.CheckAvaliability();
            }
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