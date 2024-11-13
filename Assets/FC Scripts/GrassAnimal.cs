using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GrassAnimal : MonoBehaviour
{
    public float  maxAlivetime,maxHungry;
    public float currentAlive, currentHungry;

    float hungryTime; //�C�󦹭ȶ}�l�V��
    public float searchRadius; //�V���d��
    GameObject targetGrass; //�ؼЯ�

    public Slider aliveSlider, hurgrySlider;

    NavMeshAgent agent;
    public bool isMoving = false;
    public float moveRadius; //�H�����ʽd��
    public float waitTime; //�H�����ʶ��j
    float timer; //�U�����ʮɶ�

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

        //�ͩR��slider��l�]�m
        aliveSlider.maxValue = maxAlivetime;
        aliveSlider.value = currentAlive;
        //���j��slider��l�]�m
        hurgrySlider.maxValue = maxHungry;
        hurgrySlider.value = currentHungry;

    }

    void Update()
    {
        GrassAnimalAliveTime();
        GrassAnimalHungryTime();
        
    }

    void GrassAnimalAliveTime() //�󭹰ʪ� �ͩR��
    {
        if(currentAlive > 0)
        {
            currentAlive -= Time.deltaTime;
            aliveSlider.value = currentAlive;
        }

        if(currentAlive <= 0)
        {
            Debug.Log(gameObject.name +"���`");
            Destroy(gameObject);
        }
    }

    void GrassAnimalHungryTime() //�󭹰ʪ� ���j��
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
                Debug.Log(gameObject.name + "�Q�Y��F");
                FindGrass();
            }
           
        }
        else
        {
            RandomMove();
        }


         if (currentHungry <= 0)
        {
            Debug.Log(gameObject.name + "�j��");
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
            Debug.Log(gameObject.name +"����F~~");
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
                Debug.Log(gameObject.name +"�Y���F!!");
                targetGrass = null;
            }
        }
    }

    void RandomMove() //�H���C��
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
