using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField] private int basePartDrop = 1;
    [SerializeField] private int basePointsForEnemy = 100;

    private int partDrop;
    private int pointsForEnemy;

    public void Initialize(float lootMultiplier)
    {
        partDrop = Mathf.CeilToInt(basePartDrop * lootMultiplier);
        pointsForEnemy = Mathf.CeilToInt(basePointsForEnemy * lootMultiplier);
    }

    public void DropLoot()
    {
        GameManager.Instance.PartCount += partDrop;
        GameManager.Instance.AddPoints(pointsForEnemy);
    }
}