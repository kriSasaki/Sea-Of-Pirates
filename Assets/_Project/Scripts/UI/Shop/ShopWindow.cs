using System;
using System.Collections.Generic;
using Scripts.Configs.Game;
using Scripts.Configs.UI;
using Scripts.Interfaces.Audio;
using Scripts.Systems.Shop.Items;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Shop
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

        [Inject]
        private void Construct(UiConfigs uiConfigs, GameConfig gameConfig, IAudioService audioService)
        {
            _uiConfigs = uiConfigs;
            _gameConfig = gameConfig;
            _audioService = audioService;
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