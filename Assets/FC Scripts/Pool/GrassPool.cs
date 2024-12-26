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

    [SerializeField]int grassCountSize; //�q�{�e�q
    [SerializeField]int grassMaxSize;  //������̤j�e�q�A����e�Τ��s�L���W��

    ObjectPool<Grass> grassPool;

    public Vector3 spawnArea = new Vector3(50, 0, 50); //�ͦ��d��
    public float maxFixDistance; //�ץ���̪񦳮Ħ�m���̤j�j���Z��

    public int ActiveGrassCount { get; private set; } = 0; //�ҥμƶq

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
        grassPool.Release(grass); //�եΤ��� ObjectPool �� Release ��k
        ActiveGrassCount--;
    }

    Vector3 GetValidNavMeshPosition() // ���ץ��즳�Ħ�m�A�קK�a�ũά��
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

    public void OnDestoryPoolGrass(Grass grass) //�R����H��������
    {
        Destroy(grass.gameObject);
    }

    public void OnReleasePoolGrass(Grass obj)  //����^�쪫����A���T�Ϊ��A
    {
        obj.gameObject.SetActive(false); 
        obj.isBigGrass = false;
    }

    public void OnGetPoolGrass(Grass obj)  //��q����������X�A���ҥΪ��A
    {
        obj.Initialize(this); //�ǤJ�ޥ�
      
        obj.gameObject.SetActive(true);
        obj.transform.position = GetValidNavMeshPosition();       
        ActiveGrassCount++;
    }

    public Grass OnCreatePoolGrass()  //�w�q�Ыت����������
    {
        var grass = Instantiate(grassPrefab,transform);
        grass.Initialize(this); //�T�O���T��l��

        return grass;
    }

   
   
}
