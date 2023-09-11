using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "New Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float health = 3f;
    public float speed = 4f;
    public float damage = 1f;
    public float attackSpeed = 1f;
    public float rangeOfVision = 100f;
}