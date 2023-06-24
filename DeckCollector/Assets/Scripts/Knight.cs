using UnityEngine;

public class Knight : Unit
{
    public Upgrade soldierUpgrade;
    private float attackSpeed = 1f;
    private float timer = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats();
    }

    private void Update()
    {
        if (timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

        if (target == null)
        {
            SearchForEnemy();
        }
        else
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget <= rangeOfVision)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                target = null;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (timer >= attackSpeed)
            {
                animator.SetTrigger("Attack");
                enemy.GetDamage(damage);
                timer = 0f;
            }
        }
    }

    public void SetUpStats()
    {
        Upgrade.UpgradeLevel level = soldierUpgrade.upgradeLevels[soldierUpgrade.upgradeLvl];
        maxHealth = level.hp;
        health = maxHealth;
        damage = level.dmg;
        speed = 5f;
    }

    private void SearchForEnemy()
    {
        Collider2D[] hostiles = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (Collider2D hostile in hostiles)
        {
            if (hostile.TryGetComponent<Enemy>(out Enemy enemy))
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (Vector2.Distance(transform.position, enemy.transform.position) < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy.transform;
                }
            }
        }
        target = closestEnemy;
    }
}
