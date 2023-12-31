using UnityEngine;

public class Knight : Unit
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpStats(unitData.upgrade);
        timer = attackSpeed;
    }

    private void Update()
    {
        SearchForTarget();
        if (!isDragging && timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }

        MoveTowardsTarget();
    }
}
