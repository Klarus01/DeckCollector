using UnityEngine;

public class Farm : MonoBehaviour
{
    private Animator animator;
    private float timer = 0f;
    private float interval = 5f;
    public bool isCollision;
    public int numberOfEmployees = 0;

    public int goldAvailableOnFarm = 5;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckHowManyFarmers();
        if (isCollision)
        {
            ManageGathering();
        }
    }

    private void ManageGathering()
    {
        animator.SetBool("isAction", true);
        timer += numberOfEmployees * Time.deltaTime;

        if (timer >= interval)
        {
            GameManager.Instance.GoldCount++;
            goldAvailableOnFarm--;
            if (goldAvailableOnFarm <= 0)
            {
                Destroy(gameObject);
            }
            timer = 0f;
        }
    }

    private void CheckHowManyFarmers()
    {
        numberOfEmployees = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<Farmer>(out Farmer farmer))
            {
                if (!farmer.isDragged)
                {
                    numberOfEmployees++;
                }
            }
        }
        isCollision = true;
        if (numberOfEmployees.Equals(0))
        {
            animator.SetBool("isAction", false);
            isCollision = false;
        }
    }
}