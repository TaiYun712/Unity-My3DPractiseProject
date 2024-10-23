using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public bool isBigGrass; //�O�_���j��
    public float growTime; //�ͪ��ɶ�

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


    IEnumerator GrowToGigGrass() //�p������j��
    {
        yield return new WaitForSeconds(growTime);

        if(GrassManager.instance != null && GrassManager.instance.bigGrass != null)
        {
             Instantiate(GrassManager.instance.bigGrass, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("�j�󦳰��D");
        }
        Destroy(gameObject);
    }

    public void OnEaten() //��Q�Y
    {
        if (isBigGrass && smallGrass != null)
        {
            Instantiate(smallGrass, transform.position, Quaternion.identity);
        }
        Destroy(gameObject) ;
    }
}
