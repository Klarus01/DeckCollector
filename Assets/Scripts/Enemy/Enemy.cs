using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IMovable
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected Transform target;
    [SerializeField] protected Animator animator;
    private SpriteRenderer spriteRenderer;
    protected float maxHealth;
    protected float health;
    protected float speed;
    protected float damage;
    protected float attackSpeed;
    protected float rangeOfVision;
    protected float timer;
    protected int pointsForEnemy = 100;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitializeStats();
    }

    protected virtual void Update()
    {
        if (timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

        FindClosestTarget();

        if (target != null)
        {
            MoveTowardsTarget(target);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out var targetUnit) && timer >= attackSpeed)
        {
            if (!targetUnit.isInvisible)
            {
                targetUnit.TakeDamage(damage);
                Attack();
            }
        }
    }

    protected void InitializeStats(float eliteMultiplier = 1f)
    {
        health = enemyData.health * eliteMultiplier;
        maxHealth = health;
        speed = enemyData.speed * eliteMultiplier;
        damage = enemyData.damage * eliteMultiplier;
        attackSpeed = enemyData.attackSpeed;
        rangeOfVision = enemyData.rangeOfVision;
    }

    protected void FindClosestTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (Collider2D potentialTarget in targets)
        {
            if (potentialTarget.TryGetComponent<Unit>(out Unit unit) && !unit.isInvisible)
            {
                float distance = Vector2.Distance(transform.position, unit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = unit.transform;
                }
            }
        }
        target = closestTarget;
    }

    protected void DropLoot()
    {
        GameManager.Instance.PartCount++;
        GameManager.Instance.AddPoints(pointsForEnemy);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateColor();
        if (health <= 0)
        {
            DropLoot();
            Destroy(gameObject);
        }
    }
    
    private void UpdateColor()
    {
        var healthPercentage = health / maxHealth;
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, healthPercentage);;

    }

    public virtual void MoveTowardsTarget(Transform target)
    {
        animator.SetBool("isWalking", true);
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    protected abstract void Attack();
}