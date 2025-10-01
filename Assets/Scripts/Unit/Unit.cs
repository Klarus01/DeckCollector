using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable, IMovable
{
    [SerializeField] private ParticleSystem placingParitcle;
    
    private Color originalColor;

    protected SpriteRenderer spriteRenderer;
    
    public UnitStats Stats { get; private set; }
    public UnitData unitData;
    public Animator animator;
    public int cardValue = 2;

    protected Transform target { get; set; }

    public Upgrade upgrade => unitData.upgrade;

    public event Action OnDeath;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Instantiate(placingParitcle, transform);
        InitializeStats();
    }

    private void LateUpdate()
    {
        ClampPositionToMapLimits();
    }

    public virtual void InitializeStats()
    {
        Stats = new UnitStats(
            unitData.maxHealth,
            unitData.damage,
            unitData.speed,
            unitData.rangeOfAction,
            unitData.rangeOfVision
        );
        ApplyUpgrade(unitData.upgrade);
    }

    public virtual void ApplyUpgrade(Upgrade upgrade)
    {
        var level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        Stats.ApplyModifier(level);
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

    public virtual void UnitAction() { }

    public void MoveTowardsTarget(Transform target)
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget <= Stats.RangeOfAction) UnitAction();
            else transform.position = Vector2.MoveTowards(transform.position, target.position, Stats.Speed * Time.deltaTime);
        }
    }

    public void TakeDamage(float amount)
    {
        Stats.TakeDamage(amount);
        UpdateColor();
        if (Stats.CurrentHealth <= 0)
        {
            UnitDeath();
        }
    }
    
    private void UnitDeath()
    {
        TombstoneManager.Instance.RegisterDeath(this, transform.position);
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    private void UpdateColor()
    {
        var healthPercentage = Stats.CurrentHealth / Stats.FullHealth;
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, healthPercentage);
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