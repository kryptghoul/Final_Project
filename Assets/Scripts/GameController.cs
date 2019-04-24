using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour

{
    public GameObject starfield;
    public GameObject faststarfield;
    public GameObject[] hazards;
    public AudioSource backgroundmusic;
    public AudioSource winmusic;
    public AudioSource losemusic;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float speed;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    public bool winCondition;

    public bool gameOver;
    public bool restart;
    private int score;

    private BGScroller bGScrollerObj;


    void Start()
    {
        gameOver = false;
        restart = false;
        winCondition = false;

        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        winmusic.Stop();
        losemusic.Stop();

        starfield.SetActive(true);
        faststarfield.SetActive(false);

        bGScrollerObj = GameObject.FindGameObjectWithTag("BGScroller").GetComponent<BGScroller>();
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'Z' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;

        if (score >= 100)
        {
            bGScrollerObj.scrollSpeed = -15;

            gameOverText.text = "Game created by Kay White.";
            restart = true;
            gameOver = true;
            winCondition = true;

            if (backgroundmusic.isPlaying)
            {
                backgroundmusic.Stop();
                winmusic.Play();
            }

            starfield.SetActive(false);
            faststarfield.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        if (backgroundmusic.isPlaying)
        {
            backgroundmusic.Stop();
            losemusic.Play();
        }

    }
}