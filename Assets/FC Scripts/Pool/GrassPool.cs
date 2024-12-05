using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class GrassPool : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;

    [SerializeField]int grassCountSize; //�q�{�e�q
    [SerializeField]int grassMaxSize;  //������̤j�e�q�A����e�Τ��s�L���W��

    ObjectPool<Grass> grassPool;

    public Vector3 spawnArea = new Vector3(50, 0, 50); //�ͦ��d��


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

    public void OnDestoryPoolGrass(Grass grass) //�R����H��������
    {
        Destroy(grass.gameObject);
    }

    public void OnReleasePoolGrass(Grass obj)  //����^�쪫����A���T�Ϊ��A
    {
        obj.gameObject.SetActive(false);
    }

    public void OnGetPoolGrass(Grass obj)  //��q����������X�A���ҥΪ��A
    {
        obj.gameObject.SetActive(true);
    }

    public Grass OnCreatePoolGrass()  //�w�q�Ыت����������
    {
        var grass = Instantiate(grassPrefab,transform);

        return grass;
    }

   
}
