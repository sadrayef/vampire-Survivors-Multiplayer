using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsUpBy50 : Pickup, ICollectible
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
        hero.IncreaseCoinsBy50();


        GameManager gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        hero.IncreaseCoinsBy50();
        gm.UpdateCoinsCountBy50();
        gm.UpdateCoinsCountDisplay();
    }


}
