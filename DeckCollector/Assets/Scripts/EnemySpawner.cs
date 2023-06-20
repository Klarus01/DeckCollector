using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int count;
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Enemy[] liveEnemies;
    private float timeBetweenWaves = 2f;
    public int waveNumber = 0;
    private int currentWaveIndex = 0;



    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            Debug.Log("Wave start");
            if (currentWaveIndex < waves.Length)
            {
                Wave currentWave = waves[currentWaveIndex];
                liveEnemies = new Enemy[currentWave.count];
                for (int i = 0; i < currentWave.count; i++)
                {
                    Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    Vector3 randPos = new(Random.Range(randomSpawnPoint.position.x - .7f, randomSpawnPoint.position.x + .7f), Random.Range(randomSpawnPoint.position.y - .7f, randomSpawnPoint.position.y + .7f));
                    liveEnemies[i] = Instantiate(enemy, randPos, Quaternion.identity);
                }
                currentWaveIndex++;
            }
            else
            {
                Debug.Log("Przeszed³eœ wszystkie fale!");
                break;
            }

            while (liveEnemies.Length > 0)
            {
                liveEnemies = liveEnemies.Where(enemy => enemy != null).ToArray();
                yield return new WaitForSeconds(.1f);
            }
            Debug.Log("XD");
        }
    }
}
