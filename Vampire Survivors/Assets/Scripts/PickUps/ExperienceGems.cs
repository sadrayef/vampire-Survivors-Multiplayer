using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGems : Pickup , ICollectible 
{
    public int experienceGranted;
    public override void Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        HeroStats hero = FindObjectOfType<HeroStats>();
        hero.IncreaseExperience(experienceGranted);
       
    }

    
}
