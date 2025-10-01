using UnityEngine;

public class Assassin : FighterUnit
{
    private float invisibleTimer = 0.5f;
    private float invisible = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        timer = attackSpeed;
    }

    protected override void Update()
    {
        base.Update();

        if (Stats.IsDragging)
        {
            Stats.ToggleIsInvisible(true);
            invisibleTimer = invisible;
        }

        if (!Stats.IsDragging && timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

        ManageInvisibility();
    }

    private void ManageInvisibility()
    {
        if (Stats.IsInvisible)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
            invisibleTimer -= Time.deltaTime;
            if (invisibleTimer <= 0)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                Stats.ToggleIsInvisible(false);
                invisibleTimer = invisible;
            }
        }
    }
}