using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        hero.currentMoveSpeed *= 1 + passiveItemData.Multipier / 100f; // percent    speed * 1/,,,,
    }
}
