using UnityEngine;

public class Axeman : Unit
{
    public Upgrade upgrade;
    private bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats(upgrade);
        attackSpeed = 2f;
        timer = attackSpeed;
        rangeOfAction = 2f;
    }

    private void Update()
    {
        SearchForTarget();
        if (isDragged && timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            return;
        }

        if (isDragged)
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

        isAttacking = true;

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, rangeOfAction);
        foreach (Collider2D target in targets)
        {
            if (enemy != null)
            {
                enemy.GetDamage(damage);
            }
        }
        animator.SetTrigger("Attack");
        timer = 0f;
        isAttacking = false;
    }
}
