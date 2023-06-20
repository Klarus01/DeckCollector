using UnityEngine;

public class Farmer : Unit
{
    [SerializeField] private Upgrade farmerUpgrade;

    private float closestDistance;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats();
    }

    private void Update()
    {
        if (target == null)
        {
            animator.SetBool("isGathering", false);
            SearchForFarm();
        }
        else
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget <= rangeOfVision)
            {
                if (distanceToTarget < rangeOfAction)
                {
                    animator.SetBool("isGathering", true);
                }
                else
                {
                    animator.SetBool("isGathering", false);
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
            }
            else
            {
                animator.SetBool("isGathering", false);
                target = null;
            }
        }
    }

    public void SetUpStats()
    {
        ID = 0;
        maxHealth = farmerUpgrade.hpIncrease[farmerUpgrade.upgradeLvl];
        health = maxHealth;
        speed = 3f;
    }

    private void SearchForFarm()
    {
        Collider2D[] farms = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);
        Transform closestFarm = null;
        closestDistance = Mathf.Infinity;
        foreach (Collider2D farm in farms)
        {
            if (farm.TryGetComponent<Farm>(out Farm f))
            {
                float distanceToEnemy = Vector2.Distance(transform.position, f.transform.position);
                if (Vector2.Distance(transform.position, f.transform.position) < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestFarm = f.transform;
                }
            }
        }
        target = closestFarm;
    }
}
