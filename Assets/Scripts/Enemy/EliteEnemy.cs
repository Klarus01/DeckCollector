using UnityEngine;

public class EliteEnemy : Enemy
{
    public enum EliteLevel
    {
        None,
        EliteEasy,
        EliteNormal,
        EliteHard
    }

    public EliteLevel eliteLevel = EliteLevel.None;

    protected override void Start()
    {
        base.Start();
        DetermineEliteStatus();
        ApplyEliteModifiers();
    }

    private void DetermineEliteStatus()
    {
        var chance = Random.value;

        eliteLevel = chance switch
        {
            < 0.1f => EliteLevel.EliteEasy,
            < 0.15f => EliteLevel.EliteNormal,
            < 0.2f => EliteLevel.EliteHard,
            _ => EliteLevel.None
        };
    }

    private void ApplyEliteModifiers()
    {
        var eliteMultiplier = 1f;
        switch (eliteLevel)
        {
            case EliteLevel.EliteEasy:
                partDrop = 2;
                pointsForEnemy = 200;
                eliteMultiplier = 1.25f;
                SetEliteAppearance(Color.yellow, 1.1f);
                break;
            case EliteLevel.EliteNormal:
                partDrop = 3;
                pointsForEnemy = 300;
                eliteMultiplier = 1.5f;
                SetEliteAppearance(Color.blue, 1.2f);
                break;
            case EliteLevel.EliteHard:
                partDrop = 5;
                pointsForEnemy = 500;
                eliteMultiplier = 2f;
                SetEliteAppearance(new Color(0.5f, 0f, 0.5f), 1.3f);
                break;
        }

        InitializeStats(eliteMultiplier);
    }

    private void SetEliteAppearance(Color color, float sizeMultiplier)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
        
        transform.localScale *= sizeMultiplier;
    }

    protected override void Attack() {}
}