public class MeleeEnemy : Enemy
{
    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        timer = 0f;
    }
}