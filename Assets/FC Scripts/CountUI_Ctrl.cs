using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountUI_Ctrl : MonoBehaviour
{
    //顯示 草 目前數量
    public TMP_Text grassCountText;
    GrassPool grassPool;


    void Start()
    {
        grassPool = FindObjectOfType<GrassPool>();

    }

    void Update()
    {
        grassCountText.text = grassPool.ActiveGrassCount.ToString();
    }
}
