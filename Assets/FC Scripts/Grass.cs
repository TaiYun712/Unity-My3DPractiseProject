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

    public GameObject eatGrassEffect;

    void Start()
    {
        if (!isBigGrass)
        {
          growCoroutine = StartCoroutine(GrowToBigGrass());
        }

        GrassSize();
        eatGrassEffect.SetActive(false);
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
        eatGrassEffect.SetActive(true);

        if (isBigGrass)
        {
            isBigGrass = false;
            GrassSize();
        }
        else
        {
            Destroy(gameObject);
           
        }

    }
}
