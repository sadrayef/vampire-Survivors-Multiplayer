 using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HeroStats : MonoBehaviour
{
    CharacterScriptableObject characterData;
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;


    public int savedHelath;
    public int savedSpeed;
    public int savedMagnet;

    public float maxedHelath;
    public float maxedSpeed;
    public float maxedMagnet;



    public ParticleSystem damageEffect;

    

    #region Current Stats Properties
    //properties
    public int CurrentHealth
    {
        get { return (int)currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
                    GameManager.instance.healthDisplay.text = "Health: " + currentHealth;
                }
                //Debug.Log("valus has changed");
            }
        }
    }
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
                //Debug.Log("valus has changed");
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
                }
                //Debug.Log("valus has changed");
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
                //Debug.Log("valus has changed");
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Porjectile Speed: " + currentProjectileSpeed;
                }
                //Debug.Log("valus has changed");
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value; if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
                //Debug.Log("valus has changed");
            }
        }
    }

    #endregion

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;


    [Header("Coins/PowerUp")]
    public int savingCoins = 0;


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

    [Header("UI")]
    public Image healthbar;
    public Image exBar;
    public Text levelText;

    HeroAnimator heroAnimator;


    void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();

        maxedHelath = (int)(characterData.MaxHealth + (int)PlayerPrefs.GetFloat("Health"));
        maxedSpeed = characterData.MoveSpeed + (int)PlayerPrefs.GetFloat("Speed");
        maxedMagnet = characterData.Magnet + (int)PlayerPrefs.GetFloat("Magnet");


        CurrentHealth = (int)(characterData.MaxHealth + (int)PlayerPrefs.GetFloat("Health"));
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed + (int)PlayerPrefs.GetFloat("Speed");
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet + (int)PlayerPrefs.GetFloat("Magnet");

        SpawnWeapons(characterData.StartingWeapen);
        //SpawnWeapons(secondWeaponTest);
        // SpawnPassiveItems(fisrtPassiveItemTest);

        //SpawnWeapons(secondWeaponTest);

        heroAnimator = GetComponent<HeroAnimator>();
        heroAnimator.SetAnimatorController(characterData.controller); 


    }


     void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;

        GameManager.instance.healthDisplay.text = "Health: " + currentHealth ;
        GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed ;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet ;

        GameManager.instance.AssignChosenCharacterUI(characterData);


        UpdateHealthBar();
        UpdateExBar();
        UpdateLevelText();

    }
    void Update()
    {

        savedHelath = (int)PlayerPrefs.GetFloat("Health");
        savedSpeed = (int)PlayerPrefs.GetFloat("Speed");
        savedMagnet = (int)PlayerPrefs.GetFloat("Magnet");

        if (invincibilityTimer > 0)
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

        UpdateExBar();
    }


    //IT SHOULD BE SAVED IN EVERY GAME THAT THE SAME PLAYER IS PLAY.
    public void IncreaseCoins()
    {
        Debug.Log("COIN");
        savingCoins++;
        
    }

    public void IncreaseCoinsBy50()
    {
        Debug.Log("COIN");
        savingCoins+= 50;

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

            UpdateLevelText();

            GameManager.instance.startLevelUp();
        }
    }



    void UpdateExBar()
    {
        exBar.fillAmount = (float)experience / experienceCap;
    }

    void UpdateLevelText()
    {
        levelText.text = "lvl " + level.ToString();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            CurrentHealth -= damage;

            if (damageEffect){
                Instantiate(damageEffect, transform.position, Quaternion.identity);
            }

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdateHealthBar();
            
        }
    }


    void UpdateHealthBar()
    {
        healthbar.fillAmount = currentHealth / maxedHelath;
    }


    public void Kill()
    {
        if(!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssighnChosenPassiveAndWeaponsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();

        }
    }

    public void RestoreHealth(int amount)
    {
        if(CurrentHealth < maxedHelath)
        {
            CurrentHealth += amount;

            if(CurrentHealth > maxedHelath)
            {
                CurrentHealth = (int)maxedHelath;
            }
        }

        UpdateHealthBar();

    }

    void Recover()
    {
        if(CurrentHealth < maxedHelath)
        {
           CurrentHealth += CurrentHealth * (int)(Time.deltaTime * 0.0003f);
        }
        if(CurrentHealth > maxedHelath)
        {
            CurrentHealth = (int)maxedHelath;
        }
    }

    public void SpawnPassiveItems(GameObject passiveItem)
    {

        if(passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory slots re already full!!!");
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

    public void setMaxHealth()
    {
        currentHealth += 900;
    }



}
