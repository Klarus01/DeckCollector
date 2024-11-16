public class MeleeEnemy : EliteEnemy
{
    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        ResetAttack();
    }
}