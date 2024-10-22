using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject grassAnis;
    public GameObject meatAnis;

    public int grassAnisCount; //�󭹰ʪ��ƶq
    public int meatAnisCount;  //�׭��ʪ��ƶq

    public Vector3 spawnArea = new Vector3(50, 0, 50);


    void Start()
    {
        SpawnAnimals(grassAnis,grassAnisCount);
        SpawnAnimals(meatAnis, meatAnisCount);
    }

   void SpawnAnimals(GameObject prefab,int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPos = new Vector3(
               Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
               0,
               Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
           );
            Instantiate(prefab, randomPos, Quaternion.identity);
           // Debug.Log("���ʪ��ͦ��b" + randomPos);
        }
    }
}
