using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnManager;

    // Enemy Variables
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    // Asteroid Variables
    [SerializeField]
    private GameObject _asteroidPrefab;

    // PowerUp Variables
    [SerializeField]
    private GameObject[] powerUps;

    public void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
        StartCoroutine(AsteroidSpawnRoutine());
    }

    // instantiates enemy prefab, and waits 5 seconds before spawning another enemy.
    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        while (_stopSpawning == false) {
            float randomX = Random.Range(-8f, 8f);
            Vector3 posToSpwan = new Vector3(randomX, 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpwan, Quaternion.identity);

            // organizes all enemies into the Enemy Container.
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }
    }

    // Asteroid Spawner
    IEnumerator AsteroidSpawnRoutine()
    {
        yield return new WaitForSeconds(10.0f);

        while (_stopSpawning == false) {
            float randomX = Random.Range(-8f, 8f);
            Vector3 posToSpwan = new Vector3(randomX, 9, 0);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, posToSpwan, Quaternion.identity);

            newAsteroid.transform.parent = _spawnManager.transform;

            yield return new WaitForSeconds(Random.Range(15.0f, 20.0f));
        }
    }

    // Spawns a PowerUp every 3 to 7 seconds
    IEnumerator PowerUpSpawnRoutine()
    {
        yield return new WaitForSeconds(8.0f);

        while (_stopSpawning == false) {
            float randomX = Random.Range(-8f, 8f);
            Vector3 posToSpawn = new Vector3(randomX, 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject newPowerUps = Instantiate(powerUps[randomPowerUp], posToSpawn, Quaternion.identity);

            newPowerUps.transform.parent = _spawnManager.transform;

            yield return new WaitForSeconds(Random.Range(6f, 10f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
