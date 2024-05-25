using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeat : Pickup , ICollectible 
{
    public int healthToRestore = 0;
    public void Collect()
    { 
        HeroStats hero = FindObjectOfType<HeroStats>();
        hero.RestoreHealth(healthToRestore);
        
    }

}
