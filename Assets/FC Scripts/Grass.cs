using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public bool isBigGrass; //是否為大草
    public float growTime; //生長時間

    public GameObject smallGrass;

    Coroutine growCoroutine;

    void Start()
    {
        if (!isBigGrass)
        {
          growCoroutine = StartCoroutine(GrowToGigGrass());
        }
    }

    private void OnDisable()
    {
        if (growCoroutine != null)
        {
            StopCoroutine(growCoroutine);
        }
    }


    IEnumerator GrowToGigGrass() //小草長成大草
    {
        yield return new WaitForSeconds(growTime);

        if(GrassManager.instance != null && GrassManager.instance.bigGrass != null)
        {
             Instantiate(GrassManager.instance.bigGrass, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("大草有問題");
        }
        Destroy(gameObject);
    }

    public void OnEaten() //草被吃
    {
        if (isBigGrass && smallGrass != null)
        {
            Instantiate(smallGrass, transform.position, Quaternion.identity);
        }
        Destroy(gameObject) ;
    }
}
