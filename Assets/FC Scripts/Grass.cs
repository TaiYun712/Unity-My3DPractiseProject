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

    void Start()
    {
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
        }
        else
        {
            transform.localScale = smallSize;
        }
    }

    public void OnEaten() //��Q�Y
    {
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
