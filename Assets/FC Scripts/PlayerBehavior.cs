using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    RaycastHit hitInfo;

    GrassPool grassPool;

    private void Start()
    {
        grassPool = FindObjectOfType<GrassPool>();
        ClearUnusedAssets();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckMouseClick();
            EatGrass();
        }
    }

    void CheckMouseClick() //�ˬd�I��ƻ�
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log("���I��F"+hitInfo.collider.gameObject.name);
        }
    }

    void EatGrass() 
    {
        if(hitInfo.collider.gameObject.tag == "BigGrass")
        {
            Grass grass = hitInfo.collider.GetComponent<Grass>();
            if (grass != null)
            {
                grass.OnEaten();
                Debug.Log("��Q�Y�F");
            }
        }else if(hitInfo.collider.gameObject.tag == "SmallGrass")
        {
            //  Destroy(hitInfo.collider.gameObject);
            Grass grass = hitInfo.collider.GetComponent<Grass>();
            grassPool.OnReleasePoolGrass(grass);
        }
    }

    void ClearUnusedAssets()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}
