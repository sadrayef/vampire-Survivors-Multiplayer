using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected HeroStats hero;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<HeroStats>();
        ApplyModifier();
    }
}
