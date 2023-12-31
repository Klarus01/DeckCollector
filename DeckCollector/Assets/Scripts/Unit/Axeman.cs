using UnityEngine;

public class Axeman : Unit
{
    private bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats(unitData.upgrade);
        timer = attackSpeed;
    }

    private void Update()
    {
        SearchForTarget();
        if (!isDragging && timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

        MoveTowardsTarget();
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

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, rangeOfAction);
        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.GetDamage(damage);
            }
        }
        animator.SetTrigger("Attack");
        timer = 0f;
    }
}
