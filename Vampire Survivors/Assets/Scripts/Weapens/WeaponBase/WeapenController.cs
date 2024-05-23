using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapenController : MonoBehaviour
{
    //Base Script for controlling all weapons

    [Header("Weapen Stats")]
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    float currentCooldown;
    public int pierce;

    protected HeroMovement heroMovement;


    protected virtual void Start()
    {
        heroMovement = FindObjectOfType<HeroMovement>();
        currentCooldown = cooldownDuration;
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
        currentCooldown = cooldownDuration;
    }
}
