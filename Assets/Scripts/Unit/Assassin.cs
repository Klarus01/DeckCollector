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