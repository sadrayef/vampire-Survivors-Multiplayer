using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public abstract class WeaponNewScript : MonoBehaviour
{
    [System.Serializable]

    public struct Stats
    {
        public string name, description;

        [Header("Visuals")]
        public ParticleSystem hitEffect;
        public Rect spawnVariance;

        [Header("Values")]
        public float lifespan; // if == 0 , it will last forever
        public float damage, damageVariance, area, speed, cooldown, projectileInterval, knockback;
        public int number, piercing, masInstances;


        public static Stats operator +(Stats s1, Stats s2)
        {
            Stats result = new Stats();
            result.name = s2.name ?? s1.name;
            result.description = s2.description ?? s1.description;

            result.hitEffect = s2.hitEffect == null ? s1.hitEffect : s2.hitEffect;
            result.spawnVariance = s2.spawnVariance;
            result.lifespan = s1.lifespan + s2.lifespan;
            result.damage = s1.damage + s2.damage;
            result.damageVariance = s1.damageVariance + s2.damageVariance;
            result.area = s1.area + s2.area;
            result.speed = s1.speed + s2.speed;
            result.cooldown = s1.cooldown + s2.cooldown;
            result.number = s1.number + s2.number;
            result.piercing = s1.piercing + s2.piercing;
            result.projectileInterval = s1.projectileInterval + s2.projectileInterval;
            result.knockback = s1.knockback + s2.knockback;

            return result;
        }

        public float GetDamage()
        {
            return damage + Random.Range(0, damageVariance);
        }
    }

    public int currentLevel = 1, maxLevel = 1;

    protected HeroStats owner;

    protected Stats currentStats;

    public WeaponDataNew data;

    protected HeroMovement movement;

    protected float currentCooldown;


    public virtual void Initialise(WeaponDataNew data)
    {
        maxLevel = data.maxLevel;
        owner = FindObjectOfType<HeroStats>();

        this.data = data;
        currentStats = data.baseStats;
        movement = GetComponentInParent<HeroMovement>();
        currentCooldown = currentStats.cooldown;
    }

    protected virtual void Awake()
    {
        if(data) 
        {
            currentStats = data.baseStats;
        }
    }

    protected virtual void Start()
    {
        if(data)
        {
            Initialise(data);
        }
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f)
        {
            Attack(currentStats.number);
        }
    }

    public virtual bool CanLevelUp()
    {
        return currentLevel <= maxLevel;
    }

    public virtual bool DoLevelUp()
    {
        if (!CanLevelUp())
        {
            Debug.LogWarning(string.Format("Cannot level up {0} to level {1}, max level of {2} already reached.", name, currentLevel, data.maxLevel));
            return false;
        }

        currentStats += data.GetLevelData(++currentLevel);
        return true;
    }

    public virtual bool CanAttack()
    {
        return currentCooldown <= 0;

    }

    protected virtual bool Attack(int attackCount = 1)
    {
        if(CanAttack())
        {
            currentCooldown += currentStats.cooldown;
            return true;
        }
        return false;
    }

    public virtual float GetDamage()
    {
        return currentStats.GetDamage() * owner.CurrentMight;
    }

    public virtual Stats GetStats()
    {
        return currentStats;
    }

}
