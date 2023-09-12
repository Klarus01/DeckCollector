using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData unitData;
    public Animator animator;
    protected Transform target;
    public enum UnitType
    {
        Fighter,
        Gatherer
    }

    public UnitType unitType;

    public int id;

    public float maxHealth;
    public float health;
    protected float rangeOfVision = 10f;
    protected float rangeOfAction = 1f;
    protected int damage;
    protected float attackSpeed = 1f;
    public bool isInvisible = false;
    public bool isAboveDropPoint;
    public bool isDragging = false;
    protected float speed;
    protected float timer;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDragging)
        {
            return;
        }

        if (target == null)
        {
            return;
        }

        Attack();
    }

    private void Update()
    {
        if (!isDragging)
        {
            return;
        }
    }

    public virtual void SetUpStats(Upgrade upgrade)
    {
        SetBaseStats(unitData);
        ApplyUpgrade(upgrade);
    }

    private void SetBaseStats(UnitData data)
    {
        maxHealth = data.maxHealth;
        health = maxHealth;
        damage = data.damage;
        rangeOfVision = data.rangeOfVision;
        rangeOfAction = data.rangeOfAction;
        attackSpeed = data.attackSpeed;
        speed = data.speed;
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        Upgrade.UpgradeLevel level = upgrade.upgradeLevels[upgrade.upgradeLvl];

        maxHealth = level.hp;
        health = maxHealth;
        damage = level.dmg;
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.cardManager.DropZoneOff();
            GameManager.Instance.cardManager.BackUnitToHand(this);
            Destroy(gameObject);
        }
    }

    protected void SearchForTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);

        if (unitType == UnitType.Fighter)
        {
            target = FindClosestTarget<Enemy>(targets);
        }
        else if (unitType == UnitType.Gatherer)
        {
            target = FindClosestTarget<Farm>(targets);
        }
    }

    protected Transform FindClosestTarget<T>(Collider2D[] targets) where T : Component
    {
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent<T>(out T targetComponent))
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

    public void MoveTowardsTarget()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void Attack()
    {
        if (target.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (timer >= attackSpeed)
            {
                animator.SetTrigger("Attack");
                enemy.GetDamage(damage);
                timer = 0f;
            }
        }
    }
}