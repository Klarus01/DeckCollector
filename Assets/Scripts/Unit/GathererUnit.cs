using UnityEngine;

public class GathererUnit : Unit, IGatherable
{
    public void GatherResources()
    {
        // Implement resource gathering logic
        animator.SetBool("isGathering", true);
    }

    private void Update()
    {
        //SearchForTarget<Resource>();  // Assuming there's a method for finding resources
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