using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountUI_Ctrl : MonoBehaviour
{
    //��� �� �ثe�ƶq
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
