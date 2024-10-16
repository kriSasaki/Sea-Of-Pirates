using System;
using Scripts.Configs.UI;
using Scripts.Systems.Shop.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Shop
{
    public class ShopItemSlot : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private ShopItemView _itemView;

        private ShopItem _item;

        private Action _onSelectCallback;

        private void Awake()
        {
            _selectButton.onClick.AddListener(OnItemSelected);
        }

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveListener(OnItemSelected);
        }

        public void Initialize(ShopItem item, Action onSelectCallback, ItemViewColorConfig config = null)
        {
            _item = item;
            _onSelectCallback = onSelectCallback;

            _itemView.Set(item, config);
        }

        public void CheckAvaliability()
        {
            if (_item.IsAvaliable == false)
            {
                _selectButton.interactable = false;
            }
        }

        private void OnItemSelected()
        {
            _onSelectCallback?.Invoke();
        }
    }
}