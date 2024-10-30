using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IMovable, IAttackable
{
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float health = 3f;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float rangeOfVision = 100f;

    private float timer = 1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
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
        if (!collision.gameObject.TryGetComponent<Unit>(out var target)) return;
        if (target.isInvisible) return;
        if (!(timer >= attackSpeed)) return;
        
        target.TakeDamage(damage);
        Attack(target);
    }

    protected void FindClosestTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent<Unit>(out Unit unit))
            {
                if (!unit.isInvisible)
                {
                    float distanceToUnit = Vector2.Distance(transform.position, unit.transform.position);
                    if (distanceToUnit < closestDistance)
                    {
                        closestDistance = distanceToUnit;
                        closestTarget = unit.transform;
                    }
                }
            }
        }
        target = closestTarget;
    }

    public void DropLoot()
    {
        GameManager.Instance.PartCount++;
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            DropLoot();
            Destroy(gameObject);
        }
    }

    public void MoveTowardsTarget(Transform target)
    {
        animator.SetBool("isWalking", true);
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void Attack(IDamageable target)
    {
        animator.SetTrigger("Attack");
        timer = 0f;
    }
}
