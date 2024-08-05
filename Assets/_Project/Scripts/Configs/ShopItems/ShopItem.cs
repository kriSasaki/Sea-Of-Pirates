using Project.Systems.Stats;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Configs/ShopItem")]

public class ShopItem : ScriptableObject
{
    [field: SerializeField] public GameResourceAmount Good { get; private set; }
    [field: SerializeField] public GameResourceAmount Price { get; private set; }
}