using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable, IMovable
{
    public UnitData unitData;
    public Animator animator;
    public Transform Target { get; set; }

    public bool isInvisible;
    public bool isAboveDropPoint;
    public bool isDragging;
    
    public float health;
    public float maxHealth;
    
    protected int damage;
    protected float speed;
    protected float rangeOfAction;
    protected float rangeOfVision;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        SetBaseStats();
    }
    
    protected virtual void SetBaseStats()
    {
        speed = unitData.speed;
        rangeOfAction = unitData.rangeOfAction;
        rangeOfVision = unitData.rangeOfVision;
        ApplyUpgrade(unitData.upgrade);
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        var level = upgrade.upgradeLevels[upgrade.upgradeLvl];

        maxHealth = level.hp;
        health = maxHealth;
        damage = level.dmg;
    }
    

    public void MoveTowardsTarget(Transform target)
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GameManager.Instance.cardManager.DropZoneOff();
            GameManager.Instance.cardManager.BackUnitToHand(this);
            Destroy(gameObject);
        }
    }
}