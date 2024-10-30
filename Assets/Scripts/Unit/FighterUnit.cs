using UnityEngine;

public class FighterUnit : Unit, IAttackable
{
    protected float attackSpeed;
    protected float timer;

    protected override void Awake()
    {
        base.Awake();
        attackSpeed = unitData.attackSpeed;
        timer = attackSpeed;
    }

    protected virtual void Update()
    {
        Target = SearchForTarget();
        if (timer < attackSpeed) timer += Time.deltaTime;
        if (Target) MoveTowardsTarget(Target);
    }

    protected override void SetBaseStats()
    {
        base.SetBaseStats();
        attackSpeed = unitData.attackSpeed;
    }

    private void TryAttackTarget()
    {
        if(isInvisible) return;
        if (timer >= attackSpeed && Target.TryGetComponent<IDamageable>(out var target))
        {
            Attack(target);
        }
    }

    
    public void Attack(IDamageable target)
    {
        if (!Target) return;

        animator.SetTrigger("Attack");
        target.TakeDamage(unitData.damage);
        timer = 0f;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Enemy>(out var enemy)) return;
        if (timer >= attackSpeed) TryAttackTarget();
    }

    private Transform SearchForTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent<Enemy>(out var targetComponent))
            {
                float distanceToTarget = Vector2.Distance(transform.position, targetComponent.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = targetComponent.transform;
                }
            }
        }

        return closestTarget;
    }
}