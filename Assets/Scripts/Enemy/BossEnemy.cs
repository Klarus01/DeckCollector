using UnityEngine;

public class BossEnemy : EliteEnemy
{
    [SerializeField] private bool canUseRangedAttack;
    [SerializeField] private GameObject projectilePrefab;
    
    protected override void Attack()
    {
        if (canUseRangedAttack)
        {
            LaunchProjectile();
        }
        else
        {
            animator.SetTrigger("Attack");
        }

        ResetAttackCooldown();
    }

    protected override void DropLoot()
    {
        base.DropLoot();
        pointsForEnemy = 1000;
    }

    private void LaunchProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}