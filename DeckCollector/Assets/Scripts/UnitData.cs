using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "New Unit Data")]
public class UnitData : ScriptableObject
{
    public float maxHealth;
    public float rangeOfVision;
    public float rangeOfAction;
    public float speed;
    public int damage;
    public float attackSpeed;
}