using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action - Spawns a single object at this transform's position
/// For educational use in Animation and Interactivity class.
/// Connect via UnityEvents in Inspector.
/// </summary>
public class ActionSpawnObject : MonoBehaviour
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
