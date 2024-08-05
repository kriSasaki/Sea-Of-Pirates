using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemsSheet", menuName = "Configs/ShopItemsSheet")]

public class ShopItemsSheet : ScriptableObject
{
    [SerializeField] private List<ShopItem> _shopItems;

    public IEnumerable<ShopItem> ShopItems => _shopItems;
}