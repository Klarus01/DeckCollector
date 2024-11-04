using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable, IMovable
{
    [SerializeField] private GameObject tombstonePrefab;
    [SerializeField] private ParticleSystem placingParitcle;
    protected SpriteRenderer spriteRenderer;
    protected int damage;
    protected float rangeOfAction;
    protected float rangeOfVision;
    
    private float speed;
    private Color originalColor;
    
    public UnitData unitData;
    public Animator animator;
    public bool isInvisible;
    public bool isAboveDropPoint;
    public bool isDragging;
    public float health;
    public float maxHealth;

    protected Transform Target { get; set; }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Instantiate(placingParitcle, transform);
        SetBaseStats();
    }
    
    public virtual void SetBaseStats()
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
        UpdateColor();
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
        UpdateColor();
        if (health <= 0)
        {
            UnitDeath();
        }
    }

    private void UpdateColor()
    {
        var healthPercentage = health / maxHealth;
        originalColor = Color.Lerp(Color.red, Color.white, healthPercentage);
        spriteRenderer.color = originalColor;

    }
    
    private void UnitDeath()
    {
        var tombstoneInstance = Instantiate(tombstonePrefab, transform.position, Quaternion.identity);
        tombstoneInstance.GetComponent<Tombstone>().Initialize(this);
        GameManager.Instance.cardManager.DropZoneOff();
        GameManager.Instance.deck.cardsOnBoard.Remove(this);
        Destroy(gameObject);
    }

    public void ReviveUnit()
    {
        GameManager.Instance.cardManager.BackUnitToHand(this);
    }
    
    public void SetHighlight(bool isActive)
    {
        spriteRenderer.color = isActive ? Color.yellow : originalColor;
    }
}