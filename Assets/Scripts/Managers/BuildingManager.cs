using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> existingBuildings;
    [SerializeField] private GameObject playerBuildingPrefab;
    [SerializeField] private GameObject enemyBuildingPrefab;
    [SerializeField] private GameObject treePrefab;
    private float buildingSpawnOffset = 1f;
    private int maxTrees = 120;
    private int maxTreesIncrease = 30;
    private int maxPlayerBuildings = 1;
    private int maxPlayerBuildingsIncrease = 4;
    private int maxEnemyBuildings = 1;
    private int maxEnemyBuildingsIncrease = 2;
    private float minimumDistanceBetweenBuildings = 4.5f;
    private float minimumDistanceBetweenTrees = 2f;
    private Camera mainCamera;
    
    private List<GameObject> trees = new();

    public void Initialize()
    {
        mainCamera = Camera.main;
        SpawnBuildings();
    }

    public void SpawnBuildings()
    {
        DestroyExistingBuildings();
        maxPlayerBuildings += maxPlayerBuildingsIncrease;
        maxEnemyBuildings += maxEnemyBuildingsIncrease;
        maxTrees += maxTreesIncrease;
        
        SpawnRandomBuildings(playerBuildingPrefab, maxPlayerBuildings);
        SpawnRandomBuildings(enemyBuildingPrefab, maxEnemyBuildings);
        SpawnRandomBuildings(treePrefab, maxTrees, true);
    }

    private void DestroyExistingBuildings()
    {
        foreach (var building in existingBuildings)
        {
            Destroy(building);
        }
        existingBuildings.Clear();
        
        foreach (var tree in trees)
        {
            Destroy(tree);
        }
        trees.Clear();
    }

    private void SpawnRandomBuildings(GameObject prefab, int count, bool isTree = false)
    {
        for (var i = 0; i < count; i++)
        {
            var spawnPosition = GetRandomSpawnPosition(isTree);

            if (spawnPosition != Vector3.zero)
            {
                var building = Instantiate(prefab, spawnPosition, Quaternion.identity);
                existingBuildings.Add(building);
            }
        }
    }
    
    private Vector3 GetRandomSpawnPosition(bool isTree)
    {
        const int maxAttempts = 100;

        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            var cameraHeight = 2f * mainCamera.orthographicSize;
            var cameraWidth = cameraHeight * mainCamera.aspect;

            var maxX = cameraWidth / 2f + GameManager.Instance.cameraManager.currentCameraLimits.x - 5f;
            var maxY = cameraHeight / 2f + GameManager.Instance.cameraManager.currentCameraLimits.y - 5f;
            var minX = -maxX;
            var minY = -maxY;

            var x = Random.Range(minX, maxX);
            var y = Random.Range(minY, maxY);

            var spawnPosition = new Vector3(x, y, 0f);

            if (IsPositionNearOtherObjects(spawnPosition, isTree)) return spawnPosition;
        }
        
        return Vector3.zero;
    }

    private bool IsPositionNearOtherObjects(Vector3 position, bool isTree)
    {
        foreach (var building in existingBuildings)
        {
            var distance = Vector3.Distance(position, building.transform.position);
            if (isTree && building.TryGetComponent<Tree>(out _))
            {
                if (distance < minimumDistanceBetweenTrees)
                {
                    return false;
                }
            }
            else
            {
                if (distance < minimumDistanceBetweenBuildings)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void ResetBuildings()
    {
        DestroyExistingBuildings();
        
        maxPlayerBuildings = 1;
        maxEnemyBuildings = 1;
        maxTrees = 120;
        
        SpawnBuildings();
    }
}