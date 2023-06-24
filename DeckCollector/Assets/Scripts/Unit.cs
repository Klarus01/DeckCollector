using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Animator animator;
    protected Transform target;
    public float rangeOfVision = 5f;
    public float rangeOfAction = 1f;
    public float health;
    public float maxHealth;
    public float speed;
    public int damage;
    public int id;

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