using System;
using System.Collections.Generic;
using Project.Configs.Game;
using Project.Configs.UI;
using Project.Interfaces.Audio;
using Project.Systems.Shop.Items;
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
        [SerializeField] private RectTransform _inAppHeader;

        private readonly List<ShopItemSlot> _itemSlots = new();

        private UiConfigs _uiConfigs;
        private GameConfig _gameConfig;
        private IAudioService _audioService;

        [Inject]
        public void Construct(UiConfigs uiConfigs, GameConfig gameConfig, IAudioService audioService)
        {
            _uiConfigs = uiConfigs;
            _gameConfig = gameConfig;
            _audioService = audioService;
        }

        public void Open()
        {
            Show();
        }

        public override void Hide()
        {
            base.Hide();

            _audioService.PlayMusic(_gameConfig.MainMusic);
        }

        public void HideInAppHeader()
        {
            _inAppHeader.gameObject.SetActive(false);
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

            itemSlot.Initialize(item, () => OnItemSelected(item, onBuyCallback), _uiConfigs.InApptemViewColor);

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