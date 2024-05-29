using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeat : Pickup 
{
    public int healthToRestore = 0;
    public override void Collect()
    {
        if(hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        HeroStats hero = FindObjectOfType<HeroStats>();
        hero.RestoreHealth(healthToRestore);
        
    }

}
