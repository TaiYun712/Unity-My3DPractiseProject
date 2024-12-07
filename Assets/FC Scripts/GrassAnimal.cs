using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GrassAnimal : MonoBehaviour
{
   
    public float maxAlivetime, maxHungry;
    public float currentAlive, currentHungry;

    float hungryTime; //低於此值開始覓食
    public float searchRadius; //覓食範圍
    GameObject targetGrass; //目標草
    Collider[] nearbyObject =new Collider[50];


    public Slider aliveSlider, hurgrySlider;

    NavMeshAgent agent;
    public bool isMoving = false;
    public float moveRadius; //隨機移動範圍
    public float waitTime; //隨機移動間隔
    float timer; //下次移動時間

    public enum AnimalState { FreeMove,Hungry,Dead}
    public AnimalState animalState;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        RandomLive();
        RandomHungry();
    }

    void Start()
    {
        currentAlive = maxAlivetime;
        currentHungry = maxHungry;

        //生命值slider初始設置
        aliveSlider.maxValue = maxAlivetime;
        aliveSlider.value = currentAlive;
        //飢餓值slider初始設置
        hurgrySlider.maxValue = maxHungry;
        hurgrySlider.value = currentHungry;

        hungryTime = maxHungry * 0.8f; //飽足感低於八成時開始覺得餓

        animalState = AnimalState.FreeMove;
        
    }

    void Update()
    {

        GrassAnimalAliveTime();
        GrassAnimalHungryTime();

        if (currentHungry < hungryTime) //餓了 要開始找東西吃
        {
            animalState = AnimalState.Hungry;
        }
        else
        {
            animalState = AnimalState.FreeMove;
        }

        if (currentAlive <= 0 || currentHungry <= 0) //老死或餓死
        {
            Debug.Log(gameObject.name + "死亡");
            animalState = AnimalState.Dead;
        }

        switch (animalState)
        {
            case AnimalState.FreeMove:

                RandomMove();

                break;

            case AnimalState.Hungry:

                AnimalIsHungry();
                break;

            case AnimalState.Dead:

                Destroy(gameObject);
                break;
        }

    }

    void GrassAnimalAliveTime() //草食動物 生命值
    {
        if (currentAlive > 0) //持續減少生命值 模擬老化
        {
            currentAlive -= Time.deltaTime;
            //aliveSlider.value = currentAlive;
        }

    }

    void GrassAnimalHungryTime() //草食動物 飢餓值
    {
        
        if (currentHungry > 0) //持續減少飽足感
        {
            currentHungry -= Time.deltaTime;
            hurgrySlider.value = currentHungry;
        }     
  
    }

   void AnimalIsHungry() //動物飢餓
    {
        if (targetGrass == null)
        {
            Debug.Log(gameObject.name + "想吃草了");
            FindGrass();
        }
    }

    
    void FindGrass()
    {
       
        

        if (targetGrass != null)
        {
            Debug.Log(gameObject.name + "找到草了~~");

            MoveToTargetGrass();
        }
        else
        {
            Debug.Log("找不到草  繼續遊蕩");
            RandomMove();
        }
    }

    void MoveToTargetGrass()
    {
        Vector3 grassPos = targetGrass.transform.position;
        if (targetGrass != null && targetGrass.tag == "BigGrass")
        {
            Debug.Log("移動到大草");
            agent.SetDestination(grassPos);
            
        }
        else
        {
            FindGrass();
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

                Debug.DrawLine(gameObject.transform.position, newPos, Color.red);

            }

        }


        Vector3 RandomLocation(float radius)
        {
            for (int i =0; i<10; i++)
            {
                Vector3 randomDirection = Random.insideUnitSphere * radius;
                randomDirection += transform.position;

                NavMeshHit hit;
               if( NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
                {
                    return hit.position;

                }
            }

            return transform.position;
        }
    }

    void RandomWaitTime()
    {
        waitTime = Random.Range(1f, 5f);
    }

    void RandomLive()
    {
        maxAlivetime = Random.Range(100f, 180f);
    }

    void RandomHungry()
    {
        maxHungry = Random.Range(80f, 150f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, searchRadius);

        if (targetGrass != null)
        {
            Gizmos.color = new Color(1, 0, 0, 0.25f);
            Gizmos.DrawSphere(targetGrass.transform.position, 1f);
        }
    }
}
