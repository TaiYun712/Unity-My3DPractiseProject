using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public float moveRadius; //隨機移動的半徑範圍
    public float waitTime; //等待移動間隔

    NavMeshAgent agent;
    float timer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        timer = waitTime;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= waitTime)
        {
            Vector3 newPos = RandomLocation(moveRadius);
            agent.SetDestination(newPos);
            timer = 0;
        }

        Vector3 RandomLocation(float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit,radius,1);
            return hit.position;
        }

    }
}
