using Project.Interfaces.Stats;
using Project.Systems.Stats;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class TestUpgradeSystem : MonoBehaviour
{
   private IPlayerStats _stats;

    [Inject]
    public void Construct(IPlayerStats stats)
    {
        _stats = stats;
    }

    private void Start()
    {
        Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            var s = _stats as PlayerStats;

            s.UpgradeStat(StatType.Damage);
            Debug.Log(s.GetStatLevel(StatType.Damage));
            Show();
        }
    }

    public void Show()
    {
        Debug.Log(_stats.Damage.ToString());
    }
}
