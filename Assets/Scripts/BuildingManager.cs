using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> existingBuildings;
    [SerializeField] private GameObject playerBuildingPrefab;
    [SerializeField] private GameObject enemyBuildingPrefab;
    [SerializeField] private GameObject treePrefab;
    private float buildingSpawnOffset = 1f;
    private int maxTrees = 15;
    private int maxTreesIncrese = 3;
    private int maxPlayerBuildings = 1;
    private int maxPlayerBuildingsIncrese = 2;
    private int maxEnemyBuildings = 1;
    private int maxEnemyBuildingsIncrese = 1;
    private float minimumDistanceBetweenBuildings = 5f;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        SpawnBuildings();
    }

    public void SpawnBuildings()
    {
        DestroyExistingBuildings();
        maxPlayerBuildings += maxPlayerBuildingsIncrese;
        maxEnemyBuildings += maxEnemyBuildingsIncrese;
        maxTrees += maxTreesIncrese;
        SpawnRandomBuildings(playerBuildingPrefab, maxPlayerBuildings);
        SpawnRandomBuildings(enemyBuildingPrefab, maxEnemyBuildings);
        SpawnRandomBuildings(treePrefab, maxTrees);
    }

    private void DestroyExistingBuildings()
    {
        foreach (var building in existingBuildings)
        {
            Destroy(building);
        }
        existingBuildings.Clear();
    }

    private void SpawnRandomBuildings(GameObject prefab, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var spawnPosition = GetRandomSpawnPosition();
            var building = Instantiate(prefab, spawnPosition + Vector3.up * buildingSpawnOffset, Quaternion.identity);
            existingBuildings.Add(building);
        }
    }

    private Vector3 GetRandomSpawnPosition()
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

            Vector3 spawnPosition = new(x, y, 0f);

            if (!IsPositionNearOtherBuildings(spawnPosition))
            {
                return spawnPosition;
            }
        }
        return Vector3.zero;
    }

    private bool IsPositionNearOtherBuildings(Vector3 position)
    {
        foreach (var building in existingBuildings)
        {
            var distance = Vector3.Distance(position, building.transform.position);

            if (distance < minimumDistanceBetweenBuildings)
            {
                return true;
            }
        }

        return false;
    }
    
    public void ResetBuildings()
    {
        foreach (var building in existingBuildings)
        {
            Destroy(building);
        }
        existingBuildings.Clear();
        
        maxPlayerBuildings = 1;
        maxEnemyBuildings = 1;
        maxTrees = 15;
        
        SpawnBuildings();
    }

}
