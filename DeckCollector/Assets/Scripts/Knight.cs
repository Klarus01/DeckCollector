using UnityEngine;

public class Knight : Unit
{
    public Upgrade upgrade;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats(upgrade);
        timer = attackSpeed;
    }

    private void Update()
    {
        SearchForTarget();
        if (isDragged && timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
