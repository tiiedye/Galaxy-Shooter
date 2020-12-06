using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Enemy Variables
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    // PowerUp Variables
    [SerializeField]
    private GameObject[] powerUps;

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    // instantiates enemy prefab, and waits 5 seconds before spawning another enemy.
    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false) {
            float randomX = Random.Range(-8f, 8f);
            Vector3 posToSpwan = new Vector3(randomX, 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpwan, Quaternion.identity);

            // organizes all enemies into the Enemy Container.
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
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
            Instantiate(powerUps[randomPowerUp], posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(6f, 10f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
