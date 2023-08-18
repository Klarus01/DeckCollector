using UnityEngine;

public class Assasin : Unit
{
    public Upgrade upgrade;
    public float invisible = .5f;
    private float invisibleTimer = .5f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats(upgrade);
        rangeOfVision = 2f;
        attackSpeed = .5f;
        timer = attackSpeed;
    }

    private void Update()
    {
        SearchForTarget();
        if (!isDragged && timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

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

        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void OnMouseDown()
    {
        isInvisible = true;
        invisibleTimer = Mathf.Infinity;
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        invisibleTimer = .5f;
    }
}
