using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapenController : MonoBehaviour
{
    //Base Script for controlling all weapons

    [Header("Weapen Stats")]
    public GameObject prefab;
    float currentCooldown;
    
    public WeaponScriptableObject weaponData;

    protected HeroMovement heroMovement;


    protected virtual void Start()
    {
        heroMovement = FindObjectOfType<HeroMovement>();
        currentCooldown = weaponData.CooldownDuration;
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
