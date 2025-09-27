using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour, IDamageable, IMovable
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected Transform target;
    [SerializeField] protected Animator animator;

    protected bool isAttacking;
    protected int partDrop = 1;
    protected SpriteRenderer spriteRenderer;
    protected float maxHealth;
    protected float health;
    protected float speed;
    protected float damage;
    protected float attackSpeed;
    protected float rangeOfVision;
    protected float rangeOfAttack;
    protected float timer;
    protected int pointsForEnemy = 100;

    public event Action<Enemy> OnDeath;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void InitializeStats(float eliteMultiplier = 1f)
    {
        health = enemyData.health * eliteMultiplier;
        maxHealth = health;
        speed = enemyData.speed * eliteMultiplier;
        damage = enemyData.damage * eliteMultiplier;
        attackSpeed = enemyData.attackSpeed;
        rangeOfVision = enemyData.rangeOfVision;
        rangeOfAttack = enemyData.rangeOfAttack;
    }

    protected virtual void Update()
    {
        if (timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

        if (!isAttacking)
        {
            FindClosestTarget();
        }

        if (target != null)
        {
            MoveTowardsTarget(target);
        }
    }

    public void DealDamage()
    {
        ResetAttack();

        if (target == null) return;

        var distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > rangeOfAttack) return;

        if (!target.TryGetComponent<Unit>(out var targetUnit)) return;

        if (!targetUnit.isInvisible)
        {
            targetUnit.TakeDamage(damage);
        }
    }

    protected void FindClosestTarget()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);
        var closestDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (var potentialTarget in targets)
        {
            if (potentialTarget.TryGetComponent<Unit>(out Unit unit) && !unit.isInvisible)
            {
                var distance = Vector2.Distance(transform.position, unit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = unit.transform;
                }
            }
        }
        target = closestTarget;
    }

    protected virtual void DropLoot()
    {
        GameManager.Instance.PartCount += partDrop;
        GameManager.Instance.AddPoints(pointsForEnemy);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateColor();
        if (health <= 0)
        {
            OnDeath?.Invoke(this);

            DropLoot();
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        var healthPercentage = health / maxHealth;
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, healthPercentage);
    }

    public virtual void MoveTowardsTarget(Transform target)
    {
        animator.SetBool("isWalking", true);
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (rangeOfAttack >= distanceToTarget) Attack();
        else transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    protected void ResetAttack()
    {
        isAttacking = false;
        timer = 0f;
    }

    protected abstract void Attack();
}
