public class MeleeEnemy : EliteEnemy
{
    protected override void Attack()
    {
        if (isAttacking) return;
        animator.SetTrigger("Attack");
        isAttacking = true;
    }
}