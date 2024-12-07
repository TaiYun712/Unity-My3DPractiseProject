using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    [SerializeField] GrassPool grassPool;
    [SerializeField] int minGrassCount;
    [SerializeField] int currentGrassCount;
    [SerializeField] int grassToSpawn;

    [SerializeField] float checkInterval;

    private void Start()
    {
        StartCoroutine(CheckToRespawnGrass());
    }

   
    IEnumerator CheckToRespawnGrass()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            currentGrassCount = grassPool.ActiveGrassCount;
            if (currentGrassCount < minGrassCount)
            {
                grassToSpawn = minGrassCount - currentGrassCount;

               for (int i = 0;i<grassToSpawn;i++)
                {
                    grassPool.SpawnGrass();
                }
            }

        }
    }
}
