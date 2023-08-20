using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "New Unit Data")]
public class UnitData : ScriptableObject
{
    public Unit unit;
    public Sprite cardSprite;
    public Upgrade upgrade;
    public float maxHealth;
    public float rangeOfVision;
    public float rangeOfAction;
    public float speed;
    public int damage;
    public float attackSpeed;
}