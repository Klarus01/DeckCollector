using UnityEngine;

public class UpgradeManager
{
    public static void OnUpgradeBuy(Upgrade upgrade)
    {
        if (upgrade.unit is Farmer)
        {
            Farmer[] farmers = Object.FindObjectsOfType<Farmer>();
            upgrade.upgradeLvl++;
            foreach (Farmer f in farmers)
            {
                f.SetUpStats();
            }
        }

        if (upgrade.unit is Knight)
        {
            Knight[] knights = Object.FindObjectsOfType<Knight>();
            upgrade.upgradeLvl++;
            foreach (Knight k in knights)
            {
                k.SetUpStats();
            }
        }
    }
}