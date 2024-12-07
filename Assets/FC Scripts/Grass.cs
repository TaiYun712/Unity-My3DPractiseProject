using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public bool isBigGrass =false; //是否為大草
    public float growTime; //生長時間

    public Vector3 smallSize ;
    public Vector3 bigSize ;

    Coroutine growCoroutine;

    GrassPool grassPool;



    void Start()
    {      
        isBigGrass=false;
        GrassSize();
              
        growCoroutine = StartCoroutine(GrowToBigGrass());
        

        if (grassPool == null)
        {
            grassPool = FindObjectOfType<GrassPool>();
        }
    }

    public void Initialize(GrassPool pool) //初始化
    {
        grassPool = pool;
    }

    private void OnDisable()
    {
        if (growCoroutine != null)
        {
            StopCoroutine(growCoroutine);
            growCoroutine = null;
        }
    }

   
    IEnumerator GrowToBigGrass() //小草長成大草
    {
        yield return new WaitForSeconds(growTime);
    
        isBigGrass=true;

        GrassSize();
    }

    private void Update()
    {
        GrassSize();

    }

    void GrassSize()
    {
        if (isBigGrass)
        {
            transform.localScale = bigSize;
            gameObject.tag = "BigGrass";
        }
        else
        {
            transform.localScale = smallSize;
            gameObject.tag = "SmallGrass";
            StartCoroutine(GrowToBigGrass());
        }
    }

    public void OnEaten() //草被吃
    {
        if(grassPool != null)
        {
            isBigGrass = false;         
            grassPool.Release(this);
            Debug.Log("草被吃了");
        }
        else
        {
            Debug.LogError("GrassPool 未初始化，無法回收草！");
        }
       

    }
}
