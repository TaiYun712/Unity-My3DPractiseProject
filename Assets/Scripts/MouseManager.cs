using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

public class MouseManager : MonoBehaviour
{

    RaycastHit hitInfo;

    public EventVector3 onMouseClicked;

   

    void Update()
    {
        SetCursorTexture();
        MouseCtrl();
    }


    //切換鼠標圖示
    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out hitInfo))
        {
            //切換圖示
        }
    }


    //點擊事件
    void MouseCtrl()
    {
        if(Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                onMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }


}
