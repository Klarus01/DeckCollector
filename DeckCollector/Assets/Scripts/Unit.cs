using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData unitData;
    protected Animator animator;
    protected Transform target;
    public enum UnitType
    {
        Fighter,
        Gatherer
    }

    public UnitType unitType;

    public int id;

    protected float health;
    protected float maxHealth;
    protected float rangeOfVision = 10f;
    protected float rangeOfAction = 1f;
    protected int damage;
    protected float attackSpeed = 1f;
    public bool isInvisible = false;
    public bool isDragged;
    protected float speed;
    protected float timer;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (isDragged)
            {
                return;
            }

            if (timer >= attackSpeed)
            {
                animator.SetTrigger("Attack");
                enemy.GetDamage(damage);
                timer = 0f;
            }
        }
    }

    private void OnMouseDrag()
    {
        isDragged = true;
        animator.SetBool("isDragged", true);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1f;
        transform.position = mousePos;
    }

    public virtual void OnMouseUp()
    {
        isDragged = false;
        animator.SetBool("isDragged", false);
    }

    public virtual void SetUpStats(Upgrade upgrade)
    {
        maxHealth = unitData.maxHealth;
        health = maxHealth;
        damage = unitData.damage;
        rangeOfVision = unitData.rangeOfVision;
        rangeOfAction = unitData.rangeOfAction;
        attackSpeed = unitData.attackSpeed;
        speed = unitData.speed;
        ApplyUpgrade(upgrade);
    }

    public virtual void ApplyUpgrade(Upgrade upgrade)
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
            GameManager.Instance.deck.cardsOnBoard.Remove(this);
            Destroy(gameObject);
        }
    }

    protected void SearchForTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, rangeOfVision);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (Collider2D target in targets)
        {
            if (unitType == UnitType.Fighter)
            {
                if (target.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestTarget = enemy.transform;
                    }
                }
            }
            else if (unitType == UnitType.Gatherer)
            {
                if (target.TryGetComponent<Farm>(out Farm farm))
                {
                    float distanceToFarm = Vector2.Distance(transform.position, farm.transform.position);
                    if (distanceToFarm < closestDistance)
                    {
                        closestDistance = distanceToFarm;
                        closestTarget = farm.transform;
                    }
                }
            }
        }
        target = closestTarget;
    }
}