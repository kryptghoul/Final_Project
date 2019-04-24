using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupwaves : MonoBehaviour { 

    public GameObject pickup;
    public Vector3 spawnValues;
    public int pickupCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private GameController gameControllerObj;

    private void Start()
    {
        StartCoroutine (SpawnWaves ());
        gameControllerObj = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

   IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < pickupCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(pickup, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameControllerObj.gameOver == true)
            {
                gameControllerObj.restart = true;
                break;
            }
        }
    }
}