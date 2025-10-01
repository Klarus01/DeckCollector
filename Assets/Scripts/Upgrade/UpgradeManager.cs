using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : SingletonMonobehaviour<UpgradeManager>
{
    private Dictionary<Type, Unit[]> unitDictionary;

    public void OnUpgradeBuy(Upgrade upgrade)
    {
        RefreshUnitDictionary();
        upgrade.upgradeLvl++;

        Type unitType = upgrade.unit.GetType();

        if (unitDictionary.TryGetValue(unitType, out Unit[] units))
        {
            foreach (Unit unit in units)
            {
                unit.ApplyUpgrade(upgrade);
            }
        }
    }

    private void RefreshUnitDictionary()
    {
        unitDictionary = new Dictionary<Type, Unit[]>
        {
            { typeof(Farmer), FindObjectsByType<Farmer>(FindObjectsSortMode.None) },
            { typeof(Knight), FindObjectsByType<Knight>(FindObjectsSortMode.None) },
            { typeof(Assassin), FindObjectsByType<Assassin>(FindObjectsSortMode.None) },
            { typeof(Axeman), FindObjectsByType<Axeman>(FindObjectsSortMode.None) }
        };
    }

    public void UpgradesReset()
    {
        RefreshUnitDictionary();

        foreach (var key in unitDictionary)
        {
            foreach (var unit in key.Value)
            {
                unit.InitializeStats();
            }
        }
    }
}