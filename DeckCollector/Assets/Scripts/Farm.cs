using UnityEngine;

public class Farm : MonoBehaviour
{
    private Animator animator;
    private float timer = 0f;
    private float interval = 5f;
    private bool isCollision;

    public int goldAvailableOnFarm = 5;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isCollision)
        {
            animator.SetBool("isAction", true);
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                GameManager.Instance.goldCount++;
                UIManager.Instance.UpdateUI();
                goldAvailableOnFarm--;
                if (goldAvailableOnFarm.Equals(0))
                {
                    Destroy(gameObject);
                }
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Farmer>())
        {
            isCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Farmer>())
        {
            animator.SetBool("isAction", false);
            isCollision = false;
        }
    }
}
