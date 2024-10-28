using UnityEngine;

public class FighterUnit : Unit, IAttackable
{
    protected int damage;
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
        damage = unitData.damage;
        attackSpeed = unitData.attackSpeed;
    }

    public void Attack()
    {
        if (!Target)
        {
            return;
        }
        
        if (timer >= attackSpeed && Target.TryGetComponent<IDamageable>(out var enemy))
        {
            animator.SetTrigger("Attack");
            enemy.TakeDamage(unitData.damage);
            timer = 0f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (timer >= attackSpeed) Attack();
    }

    protected Transform SearchForTarget()
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