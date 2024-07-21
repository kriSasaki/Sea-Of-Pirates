using System;
using System.Linq;
using UnityEngine;

public class TestUpgradeSystem : MonoBehaviour
{
    void Start()
    {
        System.Collections.Generic.IEnumerable<StatType> a = Enum.GetValues(typeof(StatType)).Cast<StatType>();
        

        foreach (StatType stat in a)
        {
            Debug.Log(stat.ToString());
        }
    }
}
