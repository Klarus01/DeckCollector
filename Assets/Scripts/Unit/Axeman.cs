using UnityEngine;

public class Axeman : FighterUnit
{
    private bool isAttacking = false;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        timer = attackSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Enemy>(out _))
        {
            return;
        }

        if (isDragging)
        {
            return;
        }

        if (timer < attackSpeed)
        {
            return;
        }

        if (isAttacking)
        {
            return;
        }

        PerformAttack();
        isAttacking = false;
    }

    private void PerformAttack()
    {
        isAttacking = true;

        var targets = Physics2D.OverlapCircleAll(transform.position, rangeOfAction);
        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent<IDamageable>(out var enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
        animator.SetTrigger("Attack");
        timer = 0f;
    }
}
