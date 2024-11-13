using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    RaycastHit hitInfo;
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckMouseClick();
            EatGrass();
        }
    }

    void CheckMouseClick() //檢查點到甚麼
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log("我點到了"+hitInfo.collider.gameObject.name);
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
                Debug.Log("草被吃了");
            }
        }else if(hitInfo.collider.gameObject.tag == "SmallGrass")
        {
            Destroy(hitInfo.collider.gameObject);
        }
    }
}
