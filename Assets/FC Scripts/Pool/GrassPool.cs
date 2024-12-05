using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class GrassPool : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;

    [SerializeField]int grassCountSize; //默認容量
    [SerializeField]int grassMaxSize;  //物件池最大容量，防止占用內存無限增長

    ObjectPool<Grass> grassPool;

    public Vector3 spawnArea = new Vector3(50, 0, 50); //生成範圍


    private void Awake()
    {
        grassPool = new ObjectPool<Grass>(OnCreatePoolGrass, OnGetPoolGrass, 
            OnReleasePoolGrass, OnDestoryPoolGrass, true, grassCountSize,grassMaxSize); 
    }

    private void Start()
    {
        for (int i = 0; i < grassCountSize; i++)
        {
            var grass = grassPool.Get();
            Vector3 randomPos = new Vector3(
                    Random.Range(-spawnArea.x / 2, spawnArea.x / 2), 0, Random.Range(-spawnArea.z / 2, spawnArea.z / 2));

            grass.transform.position = randomPos;
        } 
    }

    public void OnDestoryPoolGrass(Grass grass) //摧毀對象池中物件
    {
        Destroy(grass.gameObject);
    }

    public void OnReleasePoolGrass(Grass obj)  //當物件回到物件池，為禁用狀態
    {
        obj.gameObject.SetActive(false);
    }

    public void OnGetPoolGrass(Grass obj)  //當從物件池中取出，為啟用狀態
    {
        obj.gameObject.SetActive(true);
    }

    public Grass OnCreatePoolGrass()  //定義創建物件池的物件
    {
        var grass = Instantiate(grassPrefab,transform);

        return grass;
    }

   
}
