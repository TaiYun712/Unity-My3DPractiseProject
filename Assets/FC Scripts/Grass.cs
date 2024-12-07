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
        grassPool = FindObjectOfType<GrassPool>();


        if (!isBigGrass)
        {
          growCoroutine = StartCoroutine(GrowToBigGrass());
        }

        GrassSize();
        
    }

    private void OnDisable()
    {
        if (growCoroutine != null)
        {
            StopCoroutine(growCoroutine);
            growCoroutine = null;
        }
    }

    private void Update()
    {
        if (!isBigGrass)
        {
            growCoroutine = StartCoroutine(GrowToBigGrass());
        }

        GrassSize();
    }


    IEnumerator GrowToBigGrass() //�p������j��
    {
        yield return new WaitForSeconds(growTime);
    
        isBigGrass=true;

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
        }
    }

    public void OnEaten() //��Q�Y
    {

        grassPool.OnReleasePoolGrass(this);
        Debug.Log("��Q�Y�F");

    }
}
