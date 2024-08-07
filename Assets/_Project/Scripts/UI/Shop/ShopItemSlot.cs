using Project.Systems.Shop.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Shop
{
    [RequireComponent(typeof(ShopItemView))]
    public class ShopItemSlot : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;

        private ShopItemView _itemView;
        private Action _onSelectCallback;

        private void Awake()
        {
            _selectButton.onClick.AddListener(OnItemSelected);
        }

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveListener(OnItemSelected);
        }

        public void Initialize(ShopItem item, Action onSelectCallback)
        {
            _onSelectCallback = onSelectCallback;

            _itemView = GetComponent<ShopItemView>();
            _itemView.Set(item);
        }

        private void OnItemSelected()
        {
            _onSelectCallback?.Invoke();
        }
    }
}