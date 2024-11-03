using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject projectilePrefab;

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        LaunchProjectile();
        timer = 0f;
    }

    private void LaunchProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}