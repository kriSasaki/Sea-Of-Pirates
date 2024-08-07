using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemsSheet", menuName = "Configs/ShopItemsSheet")]

public class ShopItemsSheet : ScriptableObject
{
    [SerializeField] private List<GameResourceItem> _shopItems;

    public IEnumerable<GameResourceItem> ShopItems => _shopItems;
}