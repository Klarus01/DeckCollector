using System;
using System.Collections.Generic;

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

    public void RefreshUnitDictionary()
    {
        unitDictionary = new Dictionary<Type, Unit[]>
        {
            { typeof(Farmer), FindObjectsOfType<Farmer>() },
            { typeof(Knight), FindObjectsOfType<Knight>() },
            { typeof(Assasin), FindObjectsOfType<Assasin>() },
            { typeof(Axeman), FindObjectsOfType<Axeman>() }
        };
    }
}