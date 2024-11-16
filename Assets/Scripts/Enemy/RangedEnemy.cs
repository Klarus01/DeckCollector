using UnityEngine;

public class RangedEnemy : EliteEnemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private float attackCooldown = 2f;
    
    private float attackTimer;

    protected override void Update()
    {
        attackTimer += Time.deltaTime;

        FindClosestTarget();
        
        if (target != null && Vector2.Distance(transform.position, target.position) <= shootingRange)
        {
            StopMoving();

            if (!(attackTimer >= attackCooldown)) return;
            
            Attack();
            attackTimer = 0f;
        }
        else if (target != null)
        {
            MoveTowardsTarget(target);
        }
    }

    protected override void Attack()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        var projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript)
        {
            Vector2 targetDirection = (target.position - transform.position).normalized;
            var targetPosition = (Vector2)transform.position + targetDirection;
            
            projectileScript.SetTarget(targetPosition);
            projectileScript.SetDamage(damage);
        }

        animator.SetTrigger("Attack");
    }

    private void StopMoving()
    {
        animator.SetBool("isWalking", false);
    }
}