using UnityEngine;

public class RangedEnemy : EliteEnemy
{
    [SerializeField] private GameObject projectilePrefab;

    protected override void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;
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
        ResetAttackCooldown();
    }
}