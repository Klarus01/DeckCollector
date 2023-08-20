using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int count;
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private Enemy enemy;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Enemy[] liveEnemies;
    private float timeBetweenWaves = 2f;
    private int currentWaveIndex = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private void FindSpawnPoints()
    {
        spawnPoints.Clear();
        foreach (Spawner spawner in GameObject.FindObjectsOfType<Spawner>())
        {
            if (spawner.TryGetComponent<Transform>(out Transform objTransform))
            {
                spawnPoints.Add(objTransform);
            }
        }
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            Wave currentWave = waves[currentWaveIndex];
            liveEnemies = new Enemy[currentWave.count];
            FindSpawnPoints();
            for (int i = 0; i < currentWave.count; i++)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                Vector3 randPos = randomSpawnPoint.position + new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f), 0f);
                liveEnemies[i] = Instantiate(enemy, randPos, Quaternion.identity);
            }
            currentWaveIndex++;

            yield return StartCoroutine(WaitForWaveCompletion());
        }
        Debug.Log("Przeszed³eœ wszystkie fale!");
    }
    private IEnumerator WaitForWaveCompletion()
    {
        while (liveEnemies.Length > 0)
        {
            liveEnemies = liveEnemies.Where(enemy => enemy != null).ToArray();
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.Instance.cameraManager.UpdateCameraLimits();
        GameManager.Instance.buildingManager.SpawnBuildings();
    }
}
