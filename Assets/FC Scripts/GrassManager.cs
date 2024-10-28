using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public static GrassManager instance { get; private set; }

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
