using UnityEngine;

public class Farmer : Unit
{
    [SerializeField] private Upgrade upgrade;

    private float closestDistance;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats(upgrade);
        rangeOfAction = 1.5f;
    }

    private void Update()
    {
        SearchForTarget();
        if (target == null)
        {
            animator.SetBool("isGathering", false);
        }
        else
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget < rangeOfAction)
            {
                GatherResources();
            }
            else
            {
                animator.SetBool("isGathering", false);
                MoveTowardsTarget();
            }
        }
    }

    private void GatherResources()
    {
        animator.SetBool("isGathering", true);
    }
}