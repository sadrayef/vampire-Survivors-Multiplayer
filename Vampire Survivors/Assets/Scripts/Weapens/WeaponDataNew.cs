using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "2D Top-Down Rogue-Like/Weapon Data")]
public class WeaponDataNew : ScriptableObject
{
    public Sprite icon;
    public int maxLevel;

    [HideInInspector] public string behaviour;
    public WeaponNewScript.Stats baseStats;
    public WeaponNewScript.Stats[] linearGrowth;
    public WeaponNewScript.Stats[] randomGrowth;

    public WeaponNewScript.Stats GetLevelData(int level)
    {
        if(level- 2 < linearGrowth.Length)
        {
            return linearGrowth[level- 2];
        }

        if(randomGrowth.Length > 0)
        {
            return randomGrowth[Random.Range(0, randomGrowth.Length)];
        }

        Debug.LogWarning(string.Format("Weapon doesnt have its level up stats configured for level {0}!", level));
        return new WeaponNewScript.Stats();
    }
}
