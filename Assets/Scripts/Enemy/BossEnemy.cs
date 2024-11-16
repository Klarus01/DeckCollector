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

        ResetAttack();
    }

    protected override void DropLoot()
    {
        pointsForEnemy = 1000;
        base.DropLoot();
    }

    private void LaunchProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}