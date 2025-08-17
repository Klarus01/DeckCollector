using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable, IMovable
{
    [SerializeField] private GameObject tombstonePrefab;
    [SerializeField] private ParticleSystem placingParitcle;
    
    private float speed;
    private Color originalColor;
    
    protected SpriteRenderer spriteRenderer;
    protected float rangeOfAction;
    protected float rangeOfVision;
    
    public UnitData unitData;
    public Animator animator;
    public int cardValue = 2;
    public int damage;
    public float health;
    public float maxHealth;
    public bool isInvisible;
    public bool isAboveDropPoint;
    public bool isDragging;

    protected Transform Target { get; set; }

    public Upgrade upgrade => unitData.upgrade;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Instantiate(placingParitcle, transform);
        SetBaseStats();
    }

    private void LateUpdate()
    {
        ClampPositionToMapLimits();
    }

    public virtual void SetBaseStats()
    {
        speed = unitData.speed;
        rangeOfAction = unitData.rangeOfAction;
        rangeOfVision = unitData.rangeOfVision;
        ApplyUpgrade(unitData.upgrade);
    }

    public virtual void ApplyUpgrade(Upgrade upgrade)
    {
        var level = upgrade.upgradeLevels[upgrade.upgradeLvl];

        maxHealth = level.hp;
        health = maxHealth;
        damage = level.dmg;
        UpdateColor();
    }
    
    private void ClampPositionToMapLimits()
    {
        if (GameManager.Instance.cameraManager == null) return;

        var cameraLimits = GameManager.Instance.cameraManager.GetCameraLimits();
        var clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, -cameraLimits.x, cameraLimits.x),
            Mathf.Clamp(transform.position.y, -cameraLimits.y, cameraLimits.y),
            transform.position.z
        );

        transform.position = clampedPosition;
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
        GameManager.Instance.deck.cardsOnBoard.Remove(this);
        Destroy(gameObject);
    }

    public void ReviveUnit()
    {
        CardManager.Instance.BackUnitToHand(this);
    }
    
    public void SetHighlight(bool isActive)
    {
        spriteRenderer.color = isActive ? Color.yellow : originalColor;
    }
}