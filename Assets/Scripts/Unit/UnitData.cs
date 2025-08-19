using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "New Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public Unit unit;
    public Sprite cardSprite;
    public Upgrade upgrade;
    public float maxHealth;
    public float rangeOfVision;
    public float rangeOfAction;
    public float speed;
    public int damage;
    public float attackSpeed;
    public string abilityName;
    public string abilityDescription;

    public void Ability() { }
}