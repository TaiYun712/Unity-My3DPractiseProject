using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassAnimal : MonoBehaviour
{
    public float  maxAlivetime,maxHungry;
    float currentAlive, currentHungry;

    public Slider aliveSlider, hurgrySlider;

    void Start()
    {
         currentAlive = maxAlivetime;
         currentHungry= maxHungry ;

        aliveSlider.maxValue = maxAlivetime;
        aliveSlider.value = currentAlive;


    }

    void Update()
    {
        GrassAnimalAliveTime();
    }

    void GrassAnimalAliveTime()
    {
        if(currentAlive > 0)
        {
            currentAlive -= Time.deltaTime;
            aliveSlider.value = currentAlive;

        }

    }
}
