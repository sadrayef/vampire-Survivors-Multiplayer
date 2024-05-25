 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    CharacterScriptableObject characterData;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;


    public List<GameObject> spawnedWeapons;


    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;


    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

     void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;

        SpawnWeapons(characterData.StartingWeapen);
    }


     void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }
     void Update()
    {
        if(invincibilityDuration > 0)
        {
            invincibilityDuration -= Time.captureDeltaTime;
        }
        else if(isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

     void LevelUpChecker()
    {
        if(experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;

            foreach(LevelRange range in levelRanges) 
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }
    public void Kill()
    {
        Debug.Log("Dead Madar Sag !!");
    }

    public void RestoreHealth(float amount)
    {
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;

            if(currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    void Recover()
    {
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentHealth * Time.deltaTime;
        }
        if(currentHealth > characterData.MaxHealth)
        {
            currentHealth = characterData.MaxHealth;
        }
    }

    public void SpawnWeapons(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        
            spawnedWeapon.transform.SetParent(transform);
            spawnedWeapons.Add(spawnedWeapon);
        
    }
}
