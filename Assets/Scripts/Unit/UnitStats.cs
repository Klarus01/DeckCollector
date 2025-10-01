using UnityEngine;

[System.Serializable]
public class UnitStats
{
    public float FullHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float BaseDamage { get; private set; }
    public float CurrentDamage { get; private set; }
    public float Speed { get; private set; }
    public float RangeOfAction { get; private set; }
    public float RangeOfVision { get; private set; }
    public bool IsInvisible { get; private set; }
    public bool IsAboveDropPoint { get; private set; }
    public bool IsDragging { get; private set; }
    public bool IsInActionRange { get; private set; }

    public void TakeDamage(float amount) => CurrentHealth -= amount;
    public void ToggleIsInvisible(bool value) => IsInvisible = value;
    public void ToggleIsDragging(bool value) => IsDragging = value;
    public void ToggleIsAboveDropPoint(bool value) => IsAboveDropPoint = value;

    public UnitStats(float health, float damage, float speed, float rangeOfAction, float rangeOfVision)
    {
        FullHealth = health;
        CurrentHealth = health;
        BaseDamage = damage;
        CurrentDamage = damage;
        Speed = speed;
        RangeOfAction = rangeOfAction;
        RangeOfVision = rangeOfVision;
    }

    public void ApplyModifier(Upgrade.UpgradeLevel upgradeLevel)
    {
        CurrentHealth = upgradeLevel.hp;
        CurrentDamage = upgradeLevel.dmg;
    }

    public void ResetStats()
    {
        CurrentHealth = FullHealth;
        CurrentDamage = BaseDamage;
    }
}