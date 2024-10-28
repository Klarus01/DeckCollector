using UnityEngine;

public class Farmer : GathererUnit
{
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
}