using UnityEngine;

public class GathererUnit : Unit, IGatherable
{
    private float gatherCooldown;
    
    public float gatheringSpeedMultiplier = 1f;

    private void Update()
    {
        target = SearchForTarget();
        if (target) MoveTowardsTarget(target);
        else
        {
            animator.SetBool("isGathering", false);
        }
    }

    public override void ApplyUpgrade(Upgrade upgrade)
    {
        base.ApplyUpgrade(upgrade);
        var level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        gatheringSpeedMultiplier = level.gatheringSpeed;
    }

    private Transform SearchForTarget()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, Stats.RangeOfVision);

        var closestDistance = Stats.RangeOfVision;
        Transform closestTarget = null;

        foreach (var target in targets)
        {
            if (target.TryGetComponent<Farm>(out var targetComponent))
            {
                var distanceToTarget = Vector2.Distance(transform.position, targetComponent.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = targetComponent.transform;
                }
            }
        }

        return closestTarget;
    }

    public override void UnitAction()
    {
        animator.SetBool("isGathering", true);
    }
}