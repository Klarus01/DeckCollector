using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> existingBuildings;
    public GameObject goodBuildingPrefab;
    public GameObject badBuildingPrefab;
    public float buildingSpawnOffset = 1f;
    public int maxGoodBuildings = 1;
    public int maxGoodBuildingsIncrese = 2;
    public int maxBadBuildings = 1;
    public int maxBadBuildingsIncrese = 1;
    public float minimumDistanceBetweenBuildings = 5f;

    private void Start()
    {
        SpawnBuildings();
    }

    public void SpawnBuildings()
    {
        DestroyExistingBuildings();
        maxGoodBuildings += maxGoodBuildingsIncrese;
        maxBadBuildings += maxBadBuildingsIncrese;
        SpawnRandomBuildings(goodBuildingPrefab, maxGoodBuildings);
        SpawnRandomBuildings(badBuildingPrefab, maxBadBuildings);
    }

    private void DestroyExistingBuildings()
    {
        foreach (GameObject building in existingBuildings)
        {
            Destroy(building);
        }
        existingBuildings.Clear();
    }

    private void SpawnRandomBuildings(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject building = Instantiate(prefab, spawnPosition + Vector3.up * buildingSpawnOffset, Quaternion.identity);
            existingBuildings.Add(building);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int maxAttempts = 100;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Camera mainCamera = Camera.main;
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            float maxX = cameraWidth / 2f + GameManager.Instance.cameraManager.currentCameraLimits.x - 5f;
            float maxY = cameraHeight / 2f + GameManager.Instance.cameraManager.currentCameraLimits.y - 5f;
            float minX = -maxX;
            float minY = -maxY;

            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);

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
        foreach (GameObject building in existingBuildings)
        {
            float distance = Vector3.Distance(position, building.transform.position);

            if (distance < minimumDistanceBetweenBuildings)
            {
                return true;
            }
        }

        return false;
    }
}
