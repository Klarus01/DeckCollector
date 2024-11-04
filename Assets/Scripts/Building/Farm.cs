using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Farm : MonoBehaviour
{
    [SerializeField] private GameObject coinAnimPrefab;
    
    private Animator animator;
    private float timer;
    private readonly float interval = 5f;
    private bool isCollision;
    private int numberOfEmployees;
    private int goldAvailableOnFarm = 5;

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
            Instantiate(coinAnimPrefab, transform);
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
        var colliders = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach (var coll in colliders)
        {
            if (coll.TryGetComponent<Farmer>(out var farmer) && !farmer.isDragging)
            {
                numberOfEmployees++;
            }
        }

        if (numberOfEmployees.Equals(0))
        {
            animator.SetBool("isAction", false);
        }

        isCollision = numberOfEmployees > 0;
    }
}