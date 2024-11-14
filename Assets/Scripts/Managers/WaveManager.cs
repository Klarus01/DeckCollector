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
            public int count;
        }

        public List<EnemyEntry> enemies = new();
        public bool isBossWave;
    }

    [System.Serializable]
    public class Stage
    {
        public Wave[] waves = new Wave[5];
    }

    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private Stage[] stages;
    
    private List<Transform> spawnPoints = new();
    private List<Enemy> liveEnemies = new();
    private int currentStageIndex;
    private int currentWaveIndex;

    private void Start()
    {
        StartCoroutine(StartStages());
    }

    private void FindSpawnPoints()
    {
        spawnPoints.Clear();
        foreach (var spawner in FindObjectsOfType<Spawner>())
        {
            if (spawner.transform)
            {
                spawnPoints.Add(spawner.transform);
            }
        }
    }

    private IEnumerator StartStages()
    {
        while (currentStageIndex < stages.Length)
        {
            var currentStage = stages[currentStageIndex];
            for (currentWaveIndex = 0; currentWaveIndex < currentStage.waves.Length; currentWaveIndex++)
            {
                var currentWave = currentStage.waves[currentWaveIndex];
                Debug.Log($"Stage {currentStageIndex + 1}, Wave {currentWaveIndex + 1}");

                yield return new WaitForSeconds(timeBetweenWaves);
                FindSpawnPoints();

                liveEnemies.Clear();
                yield return StartCoroutine(SpawnEnemies(currentWave));
                yield return StartCoroutine(WaitForWaveCompletion());

                if (currentWave.isBossWave)
                {
                    BossWaveDefeated();
                }
            }
            currentStageIndex++;
        }

        GameCompleted();
    }

    private IEnumerator SpawnEnemies(Wave currentWave)
    {
        foreach (var enemyEntry in currentWave.enemies)
        {
            for (var i = 0; i < enemyEntry.count; i++)
            {
                var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                var randPos = randomSpawnPoint.position + new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f), 0f);

                var spawnedEnemy = Instantiate(enemyEntry.enemy, randPos, Quaternion.identity);
                liveEnemies.Add(spawnedEnemy);

                yield return new WaitForSeconds(Random.Range(0f, 0.5f));
            }
        }
    }

    private IEnumerator WaitForWaveCompletion()
    {
        while (liveEnemies.Any(enemy => enemy != null))
        {
            liveEnemies.RemoveAll(enemy => enemy == null);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void BossWaveDefeated()
    {
        GameManager.Instance.cameraManager.UpdateCameraLimits();
        GameManager.Instance.buildingManager.SpawnBuildings();
        CardManager.Instance.CollectAllCardsToHand();
    }
    
    public void ResetWaves()
    {
        currentWaveIndex = 0;
        currentStageIndex = 0;
        StopAllCoroutines();
        StartCoroutine(StartStages());
    }

    private void GameCompleted()
    {
        GameManager.Instance.ShowCompletionScreen();
    }
}
