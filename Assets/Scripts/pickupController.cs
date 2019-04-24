using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject pickup;
    public Vector3 spawnValues;
    private float multiplier = .75f;
    private float duration = 3f;

    private GameController gameControllerObj;

    private void Start()
    {
        gameControllerObj = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Instantiate(explosion, transform.position, transform.rotation);

        PlayerController delayWait = player.GetComponent<PlayerController>();
        delayWait.fireRate *= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        delayWait.fireRate /= multiplier;

        Destroy(gameObject);
    }

}