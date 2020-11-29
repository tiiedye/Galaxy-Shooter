using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // spawn game objects every 5 seconds
    // create coroutine of type IEnumerator -- yield events
    // while loop

    IEnumerator SpawnRoutine()
    {
        // while(infinite loop)
        // instantiate enemy prefab
        // yield wait for 5 seconds.
        while (true) {
            float randomX = Random.Range(-8f, 8f);
            Vector3 posToSpwan = new Vector3(randomX, 7, 0);
            Instantiate(_enemyPrefab, posToSpwan, Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }
    }
}
