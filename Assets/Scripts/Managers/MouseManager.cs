using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*�즲��k  �o�˨C��s�����N�n���s�즲
[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
*/
public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;

    RaycastHit hitInfo;

    public event Action<Vector3> OnMouseClicked;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
          
    }

    void Update()
    {
        SetCursorTexture();
        MouseCtrl();
    }


    //�������йϥ�
    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out hitInfo))
        {
            //�����ϥ�
        }
    }


    //�I���ƥ�
    void MouseCtrl()
    {
        if(Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }


}
