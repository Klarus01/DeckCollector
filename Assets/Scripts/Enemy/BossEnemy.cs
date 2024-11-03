using UnityEngine;

public class BossEnemy : Enemy
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
        timer = 0f;
    }

    private void LaunchProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}