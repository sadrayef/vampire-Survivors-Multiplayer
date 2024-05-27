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


    


    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;


    [System.Serializable]
    //nested class
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


    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;


    public GameObject fisrtPassiveItemTest, secondPassiveItemTest;

    public GameObject secondWeaponTest;


    void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();

        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;

        SpawnWeapons(characterData.StartingWeapen);
        SpawnWeapons(secondWeaponTest);
        SpawnPassiveItems(fisrtPassiveItemTest);
        SpawnPassiveItems(secondPassiveItemTest);
        
        //SpawnWeapons(secondWeaponTest);
    }


     void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }
     void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
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
        if (!isInvincible)
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
            currentHealth += currentHealth * Time.deltaTime * 0.0003f;
        }
        if(currentHealth > characterData.MaxHealth)
        {
            currentHealth = characterData.MaxHealth;
        }
    }

    public void SpawnPassiveItems(GameObject passiveItem)
    {

        if(passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Invemtory slots re already full!!!");
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        
        spawnedPassiveItem.transform.SetParent(transform);

        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());
        passiveItemIndex ++;
            
    }

    public void SpawnWeapons(GameObject weapon)
    {

        if (weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Invemtory slots re already full!!!");
            return;
        }
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);

        spawnedWeapon.transform.SetParent(transform);

        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeapenController>());
        weaponIndex++;

    }


}
