using UnityEngine;

public class Farmer : Unit
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats(unitData.upgrade);
    }

    private void Update()
    {
        SearchForTarget();
        if (target == null)
        {
            animator.SetBool("isGathering", false);
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= rangeOfAction)
        {
            GatherResources();
            return;
        }

        animator.SetBool("isGathering", false);
        MoveTowardsTarget();
    }

    private void GatherResources()
    {
        animator.SetBool("isGathering", true);
    }
}