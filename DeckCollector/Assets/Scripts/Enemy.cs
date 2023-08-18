using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Animator animator;

    private float health = 3f;
    private float speed = 4f;
    private float damage = 1f;
    private float attackSpeed = 1f;
    private float timer = 1f;
    private float rangeOfVision = 100f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }
        CheckClosestUnit();
        if (target != null)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out Unit Unit))
        {
            if (Unit.isInvisible)
            {
                return;
            }
            if (timer >= attackSpeed)
            {
                animator.SetTrigger("Attack");
                Unit.GetDamage(damage);
                timer = 0f;
            }
        }
    }

    protected void CheckClosestUnit()
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

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.partsCount++;
            GameManager.Instance.UpdateUI();
            Destroy(gameObject);
        }
    }
}
