using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        hero.currentMight *= 1 + passiveItemData.Multipier / 100f; // percent    speed * 1/,,,,
    }
}
