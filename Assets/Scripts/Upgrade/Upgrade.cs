using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Upgrades", menuName = "New Upgrade")]
public class Upgrade : ScriptableObject
{
    public Unit unit;
    public Sprite unitSprite;
    public int upgradeLvl;
    public int maxUpgradeLvl = 5;

    public int currentHealth => upgradeLevels[upgradeLvl].hp;
    public int currentDamage => upgradeLevels[upgradeLvl].dmg;

    public UpgradeLevel[] upgradeLevels;

    private void OnEnable()
    {
        upgradeLvl = 0;
    }

    [System.Serializable]
    public struct UpgradeLevel
    {
        public int costForNextLvl;
        public int hp;
        public int dmg;
        public float gatheringSpeed;
    }
}
