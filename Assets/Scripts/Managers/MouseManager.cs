using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*拖曳方法  這樣每到新場景就要重新拖曳
[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
*/
public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;

    public Texture2D point, doorway, attack, target, arrow;

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


    //切換鼠標圖示
    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out hitInfo))
        {
            //切換圖示
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
        }
    }


    //點擊事件
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
