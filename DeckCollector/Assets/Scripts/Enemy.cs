using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Animator animator;

    private int closest;
    private float shortestDist;
    private float health = 3f;
    private float speed = 4f;
    private float damage = 1f;
    private float attackSpeed = 1f;
    private float timer = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timer < attackSpeed)
        {
            timer += Time.deltaTime;
        }
        CheckClosestUnit();
        if (target != null)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out Unit Unit))
        {
            if (timer >= attackSpeed)
            {
                animator.SetTrigger("Attack");
                Unit.GetDamage(damage);
                timer = 0f;
            }
        }
    }

    private void CheckClosestUnit()
    {
        if (GameManager.Instance.unitsOnBoard.Equals(null))
        {
            animator.SetBool("isWalking", false);
            return;
        }

        shortestDist = 8f;
        closest = 0;
        for (int i = 0; i < GameManager.Instance.unitsOnBoard.Count; i++)
        {
            if (Vector2.Distance(transform.position, GameManager.Instance.unitsOnBoard[i].transform.position) < shortestDist)
            {
                closest = i;
                shortestDist = Vector2.Distance(transform.position, GameManager.Instance.unitsOnBoard[i].transform.position);
            }
            target = GameManager.Instance.unitsOnBoard[closest].transform;
        }
    }
    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.partsCount++;
            GameManager.Instance.UpdateUI();
            Destroy(gameObject);
        }
    }
}
