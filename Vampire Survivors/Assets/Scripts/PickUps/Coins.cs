using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Pickup, ICollectible
{
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
        hero.IncreaseCoins();


        GameManager gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        hero.IncreaseCoins();
        gm.UpdateCoinsCount();
        gm.UpdateCoinsCountDisplay();
    }


}
