using UnityEngine;

public class Knight : FighterUnit
{
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        timer = attackSpeed;
    }
}
