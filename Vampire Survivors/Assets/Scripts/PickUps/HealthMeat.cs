using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeat : MonoBehaviour , ICollectible 
{
    public int healthToRestore = 0;
    public void Collect()
    { 
        HeroStats hero = FindObjectOfType<HeroStats>();
        hero.RestoreHealth(healthToRestore);
        Destroy(gameObject);
    }

}
