using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades", menuName = "NewUpgrade")]
public class Upgrade : ScriptableObject
{
    public Unit unit;
    public int id;
    public string upgradeName;
    public int upgradeLvl;
    public int maxUpgradeLvl = 3;
    public int[] cost;

    public int[] hpIncrease;
    public int[] dmgIncrease;

    private void OnEnable()
    {
        upgradeLvl = 0;
    }
}
