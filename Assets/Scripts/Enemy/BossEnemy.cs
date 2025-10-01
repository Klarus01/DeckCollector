using UnityEngine;

public class BossEnemy : EliteEnemy
{
    [SerializeField] private bool canUseRangedAttack;
    [SerializeField] private GameObject projectilePrefab;

    protected override void Start()
    {
        base.Start();
        lootMultiplier = 5f;
    }

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

    private void LaunchProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}