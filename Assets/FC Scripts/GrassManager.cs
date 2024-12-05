using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public static GrassManager instance { get; private set; }
    public List<GameObject> allGrass = new List<GameObject>(); //�s�󪺦C��

    public GameObject smallGrass; //�p��
    

    public Vector3 spawnArea = new Vector3(50, 0, 50); //�ͦ��d��

    public int grassCount; //�󪺥ͦ��ƶq

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterGrass(GameObject grass)
    {
        allGrass.Add(grass);
    }

    public void UnregisterGrass(GameObject grass)
    {
        allGrass.Remove(grass);
    }

    public GameObject GetClosestGrass(Vector3 pos,float searchRadius) //�b�����
    {
        GameObject closest = null;
        float minDistance = searchRadius;

        foreach (GameObject grass in allGrass)
        {
            if(grass == null) continue;
            float distance = Vector3.Distance(pos, grass.transform.position);
            if(distance < minDistance)
            {
                closest = grass;
                minDistance = distance;
            }
        }

        return closest;
    }

    void Start()
    {
        GenerateGrass();
    }

   

    void GenerateGrass()
    {
        for (int i = 0; i < grassCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2), 0, Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
                );

            Instantiate(smallGrass, randomPos, Quaternion.identity, transform);
        }
    }
}
