using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassAnimal : MonoBehaviour
{
    public float  maxAlivetime,maxHungry;
    public float currentAlive, currentHungry;

    float hungryTime; //低於此值開始覓食

    public Slider aliveSlider, hurgrySlider;

    private void Awake()
    {
        RandomLive();
        RandomHungry();
    }

    void Start()
    {
        currentAlive = maxAlivetime;
        currentHungry= maxHungry ;

        //生命值slider初始設置
        aliveSlider.maxValue = maxAlivetime;
        aliveSlider.value = currentAlive;
        //飢餓值slider初始設置
        hurgrySlider.maxValue = maxHungry;
        hurgrySlider.value = currentHungry;

    }

    void Update()
    {
        GrassAnimalAliveTime();
        GrassAnimalHungryTime();
    }

    void GrassAnimalAliveTime()
    {
        if(currentAlive > 0)
        {
            currentAlive -= Time.deltaTime;
            aliveSlider.value = currentAlive;
        }

        if(currentAlive <= 0)
        {
            Debug.Log(gameObject.name +"死亡");
            Destroy(gameObject);
        }
    }

    void GrassAnimalHungryTime()
    {
        hungryTime = maxHungry * 0.8f;

        if(currentHungry > 0)
        {
            currentHungry -= Time.deltaTime;
            hurgrySlider.value = currentHungry;
        }

        if (currentHungry < hungryTime)
        {
            Debug.Log(gameObject.name + "想吃草了");

        }
        else if (currentHungry <= 0)
        {
            Debug.Log(gameObject.name + "餓死");
            Destroy(gameObject);
        }
    }

    void RandomLive()
    {
        maxAlivetime = Random.Range(100f,180f);
    }

    void RandomHungry()
    {
        maxHungry = Random.Range(80f,150f);
    }
}
