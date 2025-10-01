using UnityEngine;
using System.Collections.Generic;

public class Farm : MonoBehaviour
{
    [Header("Farm Settings")]
    [SerializeField] private GameObject coinAnimPrefab;
    [SerializeField] private float baseInterval = 10f;
    [SerializeField] private int maxGoldOnFarm = 5;
    [SerializeField] private float detectionRadius = 2f;

    private Animator animator;
    private float timer;
    private int currentGoldOnFarm;
    private float totalGatheringSpeed;
    private readonly HashSet<GathererUnit> gatherers = new();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentGoldOnFarm = maxGoldOnFarm;
    }

    private void Update()
    {
        UpdateGatherers();
        if (gatherers.Count > 0)
        {
            ManageGathering();
        }
        else
        {
            StopGatheringAnimation();
        }
    }

    private void ManageGathering()
    {
        animator.SetBool("isAction", true);
        timer += totalGatheringSpeed * Time.deltaTime;

        if (timer >= baseInterval)
        {
            CollectGold();
            timer = 0f;
        }
    }

    private void CollectGold()
    {
        GameManager.Instance.GoldCount++;
        currentGoldOnFarm--;
        Instantiate(coinAnimPrefab, transform);

        if (currentGoldOnFarm <= 0)
        {
            DestroyFarm();
        }
    }

    private void UpdateGatherers()
    {
        totalGatheringSpeed = 0;
    
        var colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        HashSet<GathererUnit> detectedGatherers = new HashSet<GathererUnit>();

        foreach (var coll in colliders)
        {
            if (coll.TryGetComponent<GathererUnit>(out var gatherer) && !gatherer.Stats.IsDragging)
            {
                detectedGatherers.Add(gatherer);
                if (!gatherers.Contains(gatherer))
                {
                    gatherers.Add(gatherer);
                }
                totalGatheringSpeed += gatherer.gatheringSpeedMultiplier;
            }
        }

        int removedCount = gatherers.RemoveWhere(gatherer => gatherer == null || !gatherer.isActiveAndEnabled || !detectedGatherers.Contains(gatherer));
    }

    
    private void StopGatheringAnimation()
    {
        animator.SetBool("isAction", false);
    }

    private void DestroyFarm()
    {
        Destroy(gameObject);
    }
}