using UnityEngine;

public class GathererUnit : Unit, IGatherable
{
    public void GatherResources()
    {
        animator.SetBool("isGathering", true);
    }

    private void Update()
    {
        if (Target != null && Vector2.Distance(transform.position, Target.position) <= rangeOfAction)
        {
            GatherResources();
        }
        else
        {
            animator.SetBool("isGathering", false);
            MoveTowardsTarget(Target);
        }
    }
}