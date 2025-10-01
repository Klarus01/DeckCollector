using UnityEngine;

public class Axeman : FighterUnit
{
    private void PerformAttack()
    {
        isAttacking = true;

        var targets = Physics2D.OverlapCircleAll(transform.position, Stats.RangeOfAction);
        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent<IDamageable>(out var enemy))
            {
                enemy.TakeDamage(Stats.CurrentDamage);
            }
        }
        isAttacking = false;
    }
}