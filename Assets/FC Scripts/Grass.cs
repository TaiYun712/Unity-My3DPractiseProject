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


    IEnumerator GrowToBigGrass() //小草長成大草
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

    public void OnEaten() //草被吃
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
