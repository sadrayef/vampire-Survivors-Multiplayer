using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGems : MonoBehaviour , ICollectible 
{
    public int experienceGranted;
    public void Collect()
    {
        HeroStats hero = FindObjectOfType<HeroStats>();
        hero.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }


}
