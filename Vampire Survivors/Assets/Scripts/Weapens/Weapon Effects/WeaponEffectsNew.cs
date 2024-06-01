using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEffectsNew : MonoBehaviour
{
    [HideInInspector] public HeroStats owner;
    [HideInInspector] public WeaponNewScript weapon;

    public float GetDamage()
    {
        return weapon.GetDamage();
    }
}
