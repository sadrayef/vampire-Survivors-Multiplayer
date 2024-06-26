using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------

public class DropRateManager : MonoBehaviour
{
    //---------------------------------------------------------------------

    [System.Serializable]
    //nested class
    public class Drops
    {
        public string name;
        public GameObject itemPrefab; // meat or diamond
        public float dropRate; 
    }

    //---------------------------------------------------------------------

    public List <Drops> drops;

    //---------------------------------------------------------------------

    void OnDestroy()
    {
        
        if (!gameObject.scene.isLoaded) //Debug the error in console -- isloasded = true -> play mode running     // ERROR!!!!!!!!!!!
        {
            return;
        }
        
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();
        foreach (Drops d in drops)
        {
            if (randomNumber <= d.dropRate)
            {
                possibleDrops.Add(d);
            }
        }
        if(possibleDrops.Count > 0)
        {
            Drops drop = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
        }
    }

    //---------------------------------------------------------------------

}
