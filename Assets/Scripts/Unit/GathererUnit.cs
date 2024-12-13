using UnityEngine;

public class GathererUnit : Unit, IGatherable
{
    private float gatherCooldown;
    
    public float gatheringSpeedMultiplier = 1f;

    private void Update()
    {
        if (Target != null && Vector2.Distance(transform.position, Target.position) <= rangeOfAction)
        {
            if (Time.time >= gatherCooldown)
            {
                GatherResources();
            }
        }
        else
        {
            animator.SetBool("isGathering", false);
            MoveTowardsTarget(Target);
        }
    }

    public override void ApplyUpgrade(Upgrade upgrade)
    {
        base.ApplyUpgrade(upgrade);
        var level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        gatheringSpeedMultiplier = level.gatheringSpeed;
    }

    public void GatherResources()
    {
        animator.SetBool("isGathering", true);
    }
}