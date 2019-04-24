using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starsscript : MonoBehaviour {

    private GameController gameControllerObj;
    private ParticleSystem ps;
    private float hSliderValue = 1.0F;

    void Start()
    {
        gameControllerObj = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var main = ps.main;
        main.simulationSpeed = hSliderValue;

        if (gameControllerObj.winCondition == true) ;
        {
            if (hSliderValue <=15)
            {
                hSliderValue += Time.deltaTime;
            }
        }
    }
}
