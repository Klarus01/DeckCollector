using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Animator animator;
    protected Transform target;
    protected int closest;
    protected float rangeOfVision = 5f;
    protected float rangeOfAction = .5f;
    public float health;
    public float maxHealth;
    protected float speed;
    public int damage;
    public int ID;

    private void OnMouseDrag()
    {
        animator.SetBool("isDragged", true);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
    }

    private void OnMouseUp()
    {
        animator.SetBool("isDragged", false);
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.unitsOnBoard.Remove(this);
            Destroy(gameObject);
        }
    }
}
