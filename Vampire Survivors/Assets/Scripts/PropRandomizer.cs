using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;

     void Start()
    {
         SpawnProps();
    }
     void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach (GameObject spwn in propSpawnPoints) 
        {
            int random = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[random], spwn.transform.position, Quaternion.identity);
            prop.transform.parent = spwn.transform;

        }
    }
}
