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
        switch (eliteLevel)
        {
            case EliteLevel.EliteEasy:
                lootMultiplier = 1.5f;
                SetEliteAppearance(Color.yellow, 1.1f);
                break;
            case EliteLevel.EliteNormal:
                lootMultiplier = 2f;
                SetEliteAppearance(Color.blue, 1.2f);
                break;
            case EliteLevel.EliteHard:
                lootMultiplier = 3f;
                SetEliteAppearance(new Color(0.5f, 0f, 0.5f), 1.3f);
                break;
            default:
                lootMultiplier = 1f;
                break;
        }

        InitializeStats(lootMultiplier);
    }

    private void SetEliteAppearance(Color color, float sizeMultiplier)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
        
        transform.localScale *= sizeMultiplier;
    }
}