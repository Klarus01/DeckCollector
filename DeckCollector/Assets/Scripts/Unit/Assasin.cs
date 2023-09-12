using UnityEngine;

public class Assasin : Unit
{
    public float invisible = .5f;
    private float invisibleTimer = .5f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats(unitData.upgrade);
        timer = attackSpeed;
    }

    private void Update()
    {
        SearchForTarget();

        if (isDragging)
        {
            isInvisible = true;
            invisibleTimer = invisible;
        }

        if (!isDragging && timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

        ManageInvisibility();

        MoveTowardsTarget();
    }

    private void ManageInvisibility()
    {
        if (isInvisible)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
            invisibleTimer -= Time.deltaTime;
            if (invisibleTimer <= 0)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                isInvisible = false;
                invisibleTimer = invisible;
            }
        }
    }
}
