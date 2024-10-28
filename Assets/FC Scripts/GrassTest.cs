using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTest : MonoBehaviour
{
    RaycastHit hitInfo;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ToEatGrass();

        }
    }

    void ToEatGrass()
    {
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        if(Physics.Raycast(ray,out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
        }
    }
}
