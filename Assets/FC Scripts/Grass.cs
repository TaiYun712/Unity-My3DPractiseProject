using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public bool isBigGrass =false; //�O�_���j��
    public float growTime; //�ͪ��ɶ�

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

    public void Initialize(GrassPool pool) //��l��
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

   
    IEnumerator GrowToBigGrass() //�p������j��
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

    public void OnEaten() //��Q�Y
    {
        if(grassPool != null)
        {
            isBigGrass = false;         
            grassPool.Release(this);
            Debug.Log("��Q�Y�F");
        }
        else
        {
            Debug.LogError("GrassPool ����l�ơA�L�k�^����I");
        }
       

    }
}
