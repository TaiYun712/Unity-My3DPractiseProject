using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GrassAnimal : MonoBehaviour
{
    public float  maxAlivetime,maxHungry;
    public float currentAlive, currentHungry;

    float hungryTime; //低於此值開始覓食
    public float searchRadius; //覓食範圍
    GameObject targetGrass; //目標草

    public Slider aliveSlider, hurgrySlider;

    NavMeshAgent agent;
    public bool isMoving = false;
    public float moveRadius; //隨機移動範圍
    public float waitTime; //隨機移動間隔
    float timer; //下次移動時間

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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

    void GrassAnimalAliveTime() //草食動物 生命值
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

    void GrassAnimalHungryTime() //草食動物 飢餓值
    {
        hungryTime = maxHungry * 0.8f;

        if(currentHungry > 0) 
        {
            currentHungry -= Time.deltaTime;
            hurgrySlider.value = currentHungry;
        }

        if (currentHungry < hungryTime)
        {
            if(targetGrass == null)
            {
                Debug.Log(gameObject.name + "想吃草了");
                FindGrass();
            }
           
        }
        else
        {
            RandomMove();
        }


         if (currentHungry <= 0)
        {
            Debug.Log(gameObject.name + "餓死");
            Destroy(gameObject);
        }
    }

    void FindGrass()
    {
        Collider[] nearbyObject =Physics.OverlapSphere(transform.position,searchRadius); 
        GameObject closestBigGrass = null;
        GameObject closestSmallGrass = null;

        foreach(Collider obj in nearbyObject)
        {
            if(obj.CompareTag("BigGrass"))
            {
                closestBigGrass = obj.gameObject;
            }
            else if(obj.CompareTag("SmallGrass") && closestBigGrass == null)
            {
                closestSmallGrass = obj.gameObject;
            }
        }


        targetGrass = closestBigGrass != null ? closestBigGrass : closestSmallGrass;
        if(targetGrass != null)
        {
            Debug.Log(gameObject.name +"找到草了~~");
            MoveToTargetGrass();
        }
    }

    void MoveToTargetGrass()
    {
        if (targetGrass != null)
        {
            Vector3 grassPos = targetGrass.transform.position;
            agent.SetDestination(grassPos);


            if (Vector3.Distance(transform.position, targetGrass.transform.position) < 0.5f)
            {
                Debug.Log(gameObject.name +"吃到草了!!");
                targetGrass = null;
            }
        }
    }

    void RandomMove() //隨機遊蕩
    {
        isMoving = agent.velocity.magnitude > 0.1f;

        if (!isMoving)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                Vector3 newPos = RandomLocation(moveRadius);
                agent.SetDestination(newPos);
                RandomWaitTime();
                timer = 0;
            }
        }


        Vector3 RandomLocation(float radius)
        {

            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
            return hit.position;

        }
    }

    void RandomWaitTime()
    {
        waitTime = Random.Range(1f, 5f);
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
