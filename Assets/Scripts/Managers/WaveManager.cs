using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Button nextWaveButton;
    [SerializeField] private GameObject UIStats;
    [SerializeField] private Stage[] stages;
    [SerializeField] private UIWaveManager uiWaveManager;

    private List<Transform> spawnPoints = new();
    private List<Enemy> liveEnemies = new();
    private int currentStageIndex;
    private int currentWaveIndex;
    private int totalEnemiesInWave;
    private int defeatedEnemiesInWave;
    private float timeToWaveStart = 5f;
    private bool isWaitingForNextWave = false;

    public void Initialize()
    {
        nextWaveButton.onClick.AddListener(StartNextWave);
        StartNextWave();
    }

    private void FindSpawnPoints()
    {
        spawnPoints.Clear();
        foreach (var spawner in FindObjectsByType<Spawner>(FindObjectsSortMode.None))
        {
            if (spawner.transform)
            {
                spawnPoints.Add(spawner.transform);
            }
        }
    }

    public void StartNextWave()
    {
        Time.timeScale = 1f;
        nextWaveButton.gameObject.SetActive(false);
        UIStats.SetActive(true);
        isWaitingForNextWave = false;

        if (currentStageIndex >= stages.Length)
        {
            GameCompleted();
            return;
        }

        var currentStage = stages[currentStageIndex];

        if (currentWaveIndex >= currentStage.waves.Length)
        {
            currentStageIndex++;
            currentWaveIndex = 0;

            if (currentStageIndex >= stages.Length)
            {
                GameCompleted();
                return;
            }

            currentStage = stages[currentStageIndex];
        }

        var currentWave = currentStage.waves[currentWaveIndex];
        Debug.Log($"Stage {currentStageIndex + 1}, Wave {currentWaveIndex + 1}");

        FindSpawnPoints();
        StartCoroutine(SpawnEnemies(currentWave));

        currentWaveIndex++;
    }

    private IEnumerator SpawnEnemies(Wave currentWave)
    {
        yield return new WaitForSeconds(timeToWaveStart);
        defeatedEnemiesInWave = 0;
        totalEnemiesInWave = CalculateTotalEnemiesInWave(currentWave);
        liveEnemies.Clear();

        foreach (var enemyEntry in currentWave.enemies)
        {
            for (var i = 0; i < enemyEntry.count; i++)
            {
                var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                var randPos = randomSpawnPoint.position + new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f), 0f);

                var spawnedEnemy = Instantiate(enemyEntry.enemy, randPos, Quaternion.identity);
                liveEnemies.Add(spawnedEnemy);

                spawnedEnemy.OnDeath += HandleEnemyDefeated;

                yield return new WaitForSeconds(Random.Range(0f, 0.5f));
            }
        }

        yield return StartCoroutine(WaitForWaveCompletion());

        if (currentWave.isBossWave)
        {
            BossWaveDefeated();
        }
    }

    private IEnumerator WaitForWaveCompletion()
    {
        while (liveEnemies.Any(enemy => enemy != null))
        {
            liveEnemies.RemoveAll(enemy => enemy == null);
            yield return new WaitForSeconds(0.1f);
        }

        nextWaveButton.gameObject.SetActive(true);
        UIStats.SetActive(false);
        Time.timeScale = 0f;
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

        foreach (var enemy in liveEnemies.Where(enemy => enemy != null))
        {
            Destroy(enemy.gameObject);
        }
        liveEnemies.Clear();

        isWaitingForNextWave = false;

        StartNextWave();
    }

    private void GameCompleted()
    {
        GameManager.Instance.ShowCompletionScreen();
    }

    private int CalculateTotalEnemiesInWave(Wave wave)
    {
        return wave.enemies.Sum(enemyEntry => enemyEntry.count);
    }

    private void HandleEnemyDefeated(Enemy defeatedEnemy)
    {
        defeatedEnemiesInWave++;
        liveEnemies.Remove(defeatedEnemy);
        uiWaveManager.UpdateWaveProgress(GetWaveProgress());
    }

    public float GetWaveProgress()
    {
        return totalEnemiesInWave > 0 ? (float)defeatedEnemiesInWave / totalEnemiesInWave : 0f;
    }

    public int GetTotalEnemiesInCurrentWave()
    {
        return totalEnemiesInWave;
    }

    public int GetDefeatedEnemiesInCurrentWave()
    {
        return defeatedEnemiesInWave;
    }

    public bool IsWaitingForNextWave()
    {
        return isWaitingForNextWave;
    }
}