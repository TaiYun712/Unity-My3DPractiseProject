using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
using UnityEngine.AI;

public class GrassPool : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;

    [SerializeField]int grassCountSize; //默認容量
    [SerializeField]int grassMaxSize;  //物件池最大容量，防止占用內存無限增長

    ObjectPool<Grass> grassPool;

    public Vector3 spawnArea = new Vector3(50, 0, 50); //生成範圍
    public float maxFixDistance; //修正到最近有效位置的最大搜索距離

    public int ActiveGrassCount { get; private set; } = 0; //啟用數量

    private void Awake()
    {
        grassPool = new ObjectPool<Grass>(OnCreatePoolGrass, OnGetPoolGrass, 
            OnReleasePoolGrass, OnDestoryPoolGrass, true, grassCountSize,grassMaxSize); 
    }


    private void Start()
    {
        for (int i = 0; i < grassCountSize; i++)
        {
            SpawnGrass();
        } 
    }

    public void SpawnGrass()
    {
        var grass = grassPool.Get();

    }

    public void Release(Grass grass)
    { 
        grassPool.Release(grass); //調用內部 ObjectPool 的 Release 方法
        ActiveGrassCount--;
    }

    Vector3 GetValidNavMeshPosition() // 把草修正到有效位置，避免懸空或穿模
    {
        Vector3 randomPos = new Vector3(
                    Random.Range(-spawnArea.x / 2, spawnArea.x / 2), 0
                    , Random.Range(-spawnArea.z / 2, spawnArea.z / 2));

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPos, out hit, maxFixDistance, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return randomPos;
    }

    public void OnDestoryPoolGrass(Grass grass) //摧毀對象池中物件
    {
        Destroy(grass.gameObject);
    }

    public void OnReleasePoolGrass(Grass obj)  //當物件回到物件池，為禁用狀態
    {
        obj.gameObject.SetActive(false); 
        obj.isBigGrass = false;
    }

    public void OnGetPoolGrass(Grass obj)  //當從物件池中取出，為啟用狀態
    {
        obj.Initialize(this); //傳入引用
      
        obj.gameObject.SetActive(true);
        obj.transform.position = GetValidNavMeshPosition();       
        ActiveGrassCount++;
    }

    public Grass OnCreatePoolGrass()  //定義創建物件池的物件
    {
        var grass = Instantiate(grassPrefab,transform);
        grass.Initialize(this); //確保正確初始化

        return grass;
    }

   
   
}
