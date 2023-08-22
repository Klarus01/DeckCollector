using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public float maxHealth;
    public float speed;
    public float damage;
    public float attackSpeed;
    public float rangeOfVision = 100f;
    [Range(0f, 1f)]
    public float dropChance;
}