using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _spawnSingleObject : MonoBehaviour
{

    public GameObject prefabToSpawn;
   
    public void spawnSinglePrefab()
    {

        if(prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, transform.position, transform.rotation);
        }
        

    }
}
