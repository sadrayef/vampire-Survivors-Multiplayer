using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, ICollectible
{
    protected bool hasBeenCollected = false;

    public virtual void Collect()
    {
        hasBeenCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        //NOT SURE ABOUT THIS CODE <SARA>
        if(GameManager.instance.isGameOver) 
        {
            Destroy(gameObject);
        }
    }

}
