using System;
using UnityEngine;

public class FighterUnit : Unit, IAttackable
{
    private bool isAtacking;

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
        target = SearchForTarget();
        if (timer < attackSpeed) timer += Time.deltaTime;
        if (target) MoveTowardsTarget(target);
    }

    public override void UnitAction()
    {
        if (isInvisible) return;
        if (isDragging) return;
        if (!target) return;
        Attack();
    }
    
    public void Attack()
    {
        if (isAtacking) return;
        Debug.Log("attack");
        isAtacking = true;
        animator.SetTrigger("Attack");
        timer = 0f;
    }

    private void DealDamage()
    {
        isAtacking = false;
        if (!target) return;
        target.TryGetComponent<IDamageable>(out var enemy);
        enemy.TakeDamage(unitData.damage);
    }

    private Transform SearchForTarget()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);

        var closestDistance = rangeOfVision;
        Transform closestTarget = null;

        foreach (var target in targets)
        {
            if (target.TryGetComponent<Enemy>(out var targetComponent))
            {
                var distanceToTarget = Vector2.Distance(transform.position, targetComponent.transform.position);
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