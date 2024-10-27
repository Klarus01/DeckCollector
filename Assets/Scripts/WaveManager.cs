using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        [System.Serializable]
        public class EnemyEntry
        {
            public Enemy enemy;
            [Range(0f, 1f)]
            public float spawnChance;
        }

        public int count;
        public List<EnemyEntry> enemies = new();
        public bool isBossWave;
    }


    [SerializeField] private Wave[] waves;
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private Enemy[] liveEnemies;
    private float timeBetweenWaves = 10f;
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
            Debug.Log(currentWaveIndex);
            yield return new WaitForSeconds(timeBetweenWaves);
            Wave currentWave = waves[currentWaveIndex];
            liveEnemies = new Enemy[currentWave.count];
            FindSpawnPoints();
            yield return StartCoroutine(SpawnEnemy(currentWave));
            yield return StartCoroutine(WaitForWaveCompletion());
            currentWaveIndex++;

            if (currentWave.isBossWave)
            {
                BossWaveDefeated();
            }
        }
        Debug.Log("Przeszed³eœ wszystkie fale!");
    }

    private IEnumerator SpawnEnemy(Wave currentWave)
    {
        for (int i = 0; i < currentWave.count; i++)
        {
            Enemy selectedEnemy = null;

            foreach (var enemyEntry in currentWave.enemies)
            {
                float spawnRoll = Random.Range(0f, 1f);
                if (spawnRoll <= enemyEntry.spawnChance)
                {
                    selectedEnemy = enemyEntry.enemy;
                    break;
                }
                spawnRoll -= enemyEntry.spawnChance;
            }

            if (selectedEnemy != null)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                Vector3 randPos = randomSpawnPoint.position + new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f), 0f);
                liveEnemies[i] = Instantiate(selectedEnemy, randPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        }
    }

    private IEnumerator WaitForWaveCompletion()
    {
        while (liveEnemies.Length > 0)
        {
            liveEnemies = liveEnemies.Where(enemy => enemy != null).ToArray();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void BossWaveDefeated()
    {
        GameManager.Instance.cameraManager.UpdateCameraLimits();
        GameManager.Instance.buildingManager.SpawnBuildings();
        GameManager.Instance.cardManager.CollectAllCardsToHand();
    }
}
